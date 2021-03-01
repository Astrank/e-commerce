var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        objectIndex: 0,
        products: [],
        stock: [],
        productModel: {
            id: 0,
            name: "asd",
            description: "dsa",
            value: 1.77,
            stock: []
        },
        file: "",

        selectedStock: null,
        newStock: {
            productId: 0,
            description: "",
            qty: ""
        },
        totalStock: []
    },
    mounted(){
        this.getProducts();
        //this.getStock();

        console.log(this.products);
    },
    methods: {
        getFile(event) {
            this.file = event.target.files[0];
            console.log(this.file);
            console.log(this.productModel);
        },
        submitForm(event){
            event.preventDefault();
            let formData = new FormData();
            formData.append("file", this.file);

            axios.post('/Products', formData)
            .then(res => {
                console.log(res.data);
                this.products.push(res.data);
            })
            .catch(err => {
                console.log(err);
            })
            .then(() => {
                this.editing = false;
            });
        },
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
        },
        createProduct(){
            var formData = new FormData();

            formData.append("image", this.file);
            formData.append("id", this.productModel.id)
            formData.append("name", this.productModel.name)
            formData.append("description", this.productModel.description)
            formData.append("value", this.productModel.value)

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
                    this.editing = false;
                });
        },
        updateProduct(){
            console.log(this.productModel);
            var formData = new FormData();

            if (this.file != null || this.file == "") {
                formData.append("image", this.file);
            }

            formData.append("id", this.productModel.id)
            formData.append("name", this.productModel.name)
            formData.append("description", this.productModel.description)
            formData.append("value", this.productModel.value)

            console.log(formData);

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
                    this.editing = false;
                });
        },
        deleteProduct(id, index){
            this.loading = true;
            axios.delete('/products/' + id)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
          this.editing = true;  
          this.productModel = {
            id: 0,
            name: "",
            description: "",
            value: 0,
            stock: []
          }
        },
        editProduct(id, index){
            this.objectIndex = index;

            this.products.forEach(x => {
                if (x.id == id) {
                    this.productModel = {
                        id: x.id,
                        name: x.name,
                        description: x.description,
                        value: x.value,
                        stock: x.stock
                    }
                }
            });

            this.editing = true;
        },
        cancel() {
            this.editing = false;
        },

        getStock(stock) {
            var totalS = 0;
            stock.forEach(s => {
                totalS += s.qty;
            });
            return totalS;
            /*this.loading = true;
            axios.get('/stocks')
                .then(res => {
                    this.stock = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });*/
        },
        productStock(id){
            this.stock.forEach(x => {
                if (x.id == id) {
                    this.selectedStock = x;
                }
            });
        },
        addStock() {
            this.loading = true;
            axios.post('/stocks', this.newStock)
                .then(res => {
                    this.selectedProduct.stock.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        
        /*
        
        getProduct(id){
        this.loading = true;
        axios.get('/Products/' + id)
            .then(res => {
                console.log(res);
                var product = res.data;
                this.productModel = {
                    id: product.id,
                    name: product.name,
                    description: product.description,
                    value: product.value
                }; 
            })
            .catch(err => {
                console.log(err);
            })
            .then(() => {
                this.loading = false;
            });
        },
        
        */
    },
    computed: {
    }
})