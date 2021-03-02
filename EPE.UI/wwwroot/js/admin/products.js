var app = new Vue({
    el: '#app',
    data: {
        showList: false,
        showProduct: false,
        showStock: false,
        loading: false,
        objectIndex: 0,
        
        products: [],
        productModel: {
            id: 0,
            name: "",
            description: "",
            value: 0,
            stock: [],
        },
        file: "",

        selectedStock: null,
        
        stock: [],
        stockModel: {
            productId: 0,
            description: "",
            qty: ""
        },
    },
    mounted(){
        this.getProducts();
    },
    methods: {
        getProducts(){
            this.loading = true;
            axios.get('/Products')
                .then(res => {
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleList();
        },
        newProduct() {
            this.productModel = {
              id: 0,
              name: "",
              description: "",
              value: 0,
              stock: []
            }

            this.toggleProduct();
        },
        getFile(event) {
            this.file = event.target.files[0];
        },
        createProduct(){
            var formData = new FormData();

            formData.append("image", this.file);
            formData.append("id", this.productModel.id);
            formData.append("name", this.productModel.name);
            formData.append("description", this.productModel.description);
            formData.append("value", this.productModel.value);

            this.loading = true;
            axios.post('/Products', formData,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleList();
        },
        updateProduct(){
            var formData = new FormData();

            if (this.file != null || this.file == "") {
                formData.append("image", this.file);
            };

            formData.append("id", this.productModel.id);
            formData.append("name", this.productModel.name);
            formData.append("description", this.productModel.description);
            formData.append("value", this.productModel.value);

            this.loading = true;
            axios.put('/Products', formData,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleList();
        },
        deleteProduct(id){
            this.loading = true;
            axios.delete('/products/' + id)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleList();
        },
        editProduct(product, index){
            this.objectIndex = index;
            this.selectedProduct = product;

            this.products.forEach(x => {
                if (x.id == product.id) {
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value,
                        stock: product.stock,                                        
                    }
                }
            });

            this.toggleProduct();
        },

        /* ---- */

        getStock(stock) {
            var totalS = 0;
            if (stock != null) {
                stock.forEach(s => {
                    totalS += s.qty;
                });
            }
            return totalS;
        },
        productStock(id){
            this.stock.forEach(x => {
                if (x.id == id) {
                    this.selectedStock = x;
                }
            });
        },
        addStock() {
            console.log(this.productModel.id);
            this.productModel.stock.push({
                productId: this.selectedProduct.id,
                description: this.stockModel.description,
                qty: this.stockModel.qty
            });
            console.log(this.productModel.stock);

            this.stockModel = {
                productId: 0,
                description: "",
                qty: ""
            };

            //console.log(this.productModel);

            /*this.loading = true;
            axios.post('/stocks', this.stockModel)
                .then(res => {
                    this.selectedProduct.stock.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });*/
        },
        updateStock() {
            this.loading = true;
            axios.put('/stocks', {
                stock: this.productModel.stock.map(x => {
                    return {
                        id: x.id,
                        description: x.description,
                        qty: x.qty,
                        productId: this.productModel.id
                    };
                })
            })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleProduct();
        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete('/stocks/' + id)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        toggleList(){
            this.showStock = false;
            this.showProduct = false;
            this.showList = true;
        },

        toggleProduct(){
            this.showStock = false;
            this.showList = false;
            this.showProduct = true;
        },

        toggleStock(){
            this.showProduct = false;
            this.showList = false;
            this.showStock = true;
        },
    },
    computed: {
    }
})