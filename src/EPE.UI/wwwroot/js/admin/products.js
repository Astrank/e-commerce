var app = new Vue({
    el: "#app",
    data: {
        showList: true,
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
            subcategoryId: null,
            stock: [],
            primaryImage: "",
            images: null,
        },
        primaryImageFile: null,
        imageFiles: null,

        selectedStock: null,
        
        stock: [],
        stockModel: {
            productId: 0,
            description: "",
            qty: ""
        },

        subcategories: []
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
        getProduct(id, index){
            this.loading = true;
            this.objectIndex = index;

            axios.get('/Products/' + id)
                .then(res => {
                    console.log(res);
                    this.productModel = {
                        id: res.data.id,
                        name: res.data.name,
                        description: res.data.description,
                        value: res.data.value,
                        subcategoryId: res.data.subcategoryId,
                        primaryImage: res.data.primaryImage,
                        images: res.data.images
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleProduct();
        },
        newProduct() {
            this.productModel = {
              id: 0,
              name: "",
              description: "",
              value: 0,
              stock: [],
              subcategoryId: null
            };

            this.toggleProduct();
        },
        getPrimaryImage(event) {
            this.primaryImageFile = event.target.files[0];
        },
        getImages(event) {
            this.imageFiles = event.target.files;
        },
        createProduct() {
            var formData = new FormData();

            formData.append("id", this.productModel.id);
            formData.append("name", this.productModel.name);
            formData.append("description", this.productModel.description);
            formData.append("value", this.productModel.value);
            formData.append("subcategoryId", this.productModel.subcategoryId);
            formData.append("primaryImageFile", this.primaryImageFile);

            if (this.imageFiles != null) {
                for (let i = 0; i < this.imageFiles.length; i++) {
                    formData.append("imageFiles", this.imageFiles[i]);
                }
            };

            this.loading = true;
            axios.post('/Products', formData,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(res => {
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

            formData.append("id", this.productModel.id);
            formData.append("name", this.productModel.name);
            formData.append("description", this.productModel.description);
            formData.append("value", this.productModel.value);
            formData.append("primaryImage", this.productModel.primaryImage);
            formData.append("primaryImageFile", this.primaryImageFile);

            if (this.productModel.images != null) {
                for (let i = 0; i < this.productModel.images.length; i++) {
                    formData.append("images", this.productModel.images[i]);
                }
            }
            
            if (this.imageFiles) {
                for (let i = 0; i < this.imageFiles.length; i++) {
                    formData.append("imageFiles", this.imageFiles[i]);
                }
            }


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

        /* STOCK METHODS */

        getStock(id) {
            this.loading = true;

            axios.get('/stocks/' + id)
                .then(res => {
                    this.stock = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleStock();
        },
        addStock() {
            this.stock.push({
                productId: this.productModel.id,
                description: this.stockModel.description,
                qty: this.stockModel.qty
            });

            console.log(this.stock);
    
            this.stockModel = {
                productId: 0,
                description: "",
                qty: ""
            };
        },
        updateStock() {
            this.loading = true;
            axios.put('/stocks', {
                stock: this.stock.map(x => {
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
                    this.stock.splice(index, 1);
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
            this.loading = true;

            axios.get("/Subcategories")
                .then(res => {
                    this.subcategories = res.data
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

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