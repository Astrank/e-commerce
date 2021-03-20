var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        isCategory: true,
        isSubcategory: false,
        showList: true,
        showElement: false,

        status: 0,
        objectIndex: 0,

        categories: [],
        subcategories: [],

        categoryModel: {
            id: null,
            name: ""
        },
        subcategoryModel: {
            id: null,
            name: "",
            categoryId: null
        },
    },
    mounted() {
        this.getCategories();
    },
    methods: {

        // CATEGORIES
            
        newCategory() {
            this.categoryModel = {
                name: ""
            }

            this.toggleCategoryElement();
        },
        createCategory() {
            this.loading = true;

            var request = new FormData();
            request.append("name", this.categoryModel.name);

            axios.post("/Categories", request)
                .then(res => {
                    this.categories.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleCategoryList();
        },
        getCategories() {
            this.loading = true;

            axios.get("/Categories")
                .then(res => {
                    this.categories = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleCategoryList();
        },
        getCategory(index){
            this.categoryModel.id = this.categories[index].id;
            this.categoryModel.name = this.categories[index].name;

            this.toggleCategoryElement();
        },
        updateCategory() {
            this.loading = true;

            var request = new FormData();
            request.append("id", this.categoryModel.id);
            request.append("name", this.categoryModel.name);

            axios.put("/Categories", request)
                .then(res => {
                    this.categories.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleCategoryList();
        },
        deleteCategory(id) {
            this.loading = true;

            axios.delete("/Categories/" + id)
                .then(res => {
                    this.categories.splice(this.objectIndex, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleCategoryList();
        },

        // SUBCATEGORIES

        newSubcategory() {
            this.subcategoryModel = {
                name: "",
                categoryId: null
            };

            this.toggleSubcategoryElement();
        },
        createSubcategory() {
            this.loading = true;

            var request = new FormData();
            request.append("name", this.subcategoryModel.name);
            request.append("categoryId", this.subcategoryModel.categoryId);

            axios.post("/Subcategories/", request)
                .then(res => {
                    this.subcategories.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleSubcategoryList();
        },
        getSubcategories() {
            this.loading = true;

            axios.get("/Subcategories")
                .then(res => {
                    this.subcategories = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleSubcategoryList();
        },
        getSubcategory(index){
            this.subcategoryModel.id = this.subcategories[index].id;
            this.subcategoryModel.name = this.subcategories[index].name;
            this.subcategoryModel.categoryId = this.subcategories[index].categoryId;

            this.toggleSubcategoryElement();
        },
        updateSubcategory() {
            this.loading = true;

            var request = new FormData();
            request.append("id", this.subcategoryModel.id);
            request.append("name", this.subcategoryModel.name);
            request.append("categoryId", this.subcategoryModel.categoryId);

            axios.put("/Subcategories", request)
                .then(res => {
                    this.subcategories.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleSubcategoryList();
        },
        deleteSubcategory(id) {
            this.loading = true;

            axios.delete("/Subcategories/" + id)
                .then(res => {
                    this.subcategories.splice(this.objectIndex, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleSubcategoryList();
        },

        toggleCategoryList(){
            this.status = 0;

            this.isCategory = true;
            this.isSubcategory = false;

            this.showList = true;
            this.showElement = false;
        },
        toggleCategoryElement(){
            this.isCategory = true;
            this.isSubcategory = false;

            this.showList = false;
            this.showElement = true;
        },
        toggleSubcategoryList(){
            this.status = 1;

            this.isCategory = false;
            this.isSubcategory = true;

            this.showList = true;
            this.showElement = false;
        },
        toggleSubcategoryElement(){
            this.isCategory = false;
            this.isSubcategory = true;

            this.showList = false;
            this.showElement = true;
        },
    }
})