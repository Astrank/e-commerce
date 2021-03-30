    var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        adding: true,
        updating: false,
        isCategory: true,
        isSubcategory: false,
        showList: true,
        showElement: false,
        
        parentIds: [],

        objectIndex: 0,

        categories: [],
        subcategories: [],
        categoryList: [],

        categoryModel: {
            id: null,
            name: "",
            parentId: null
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
        getCategories() {
            this.loading = true;

            axios.get("/Categories")
                .then(res => {
                    this.categories = res.data.sort(function (a, b) { return a.parent.id - b.parent.id });
                    this.getChildren(0);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.toggleCategoryList();
        },
        getChildren(parentId){
            var newList = [];
            
            this.categories.forEach(c => {
                if (c.parent.id == parentId) {
                    newList.push(c);
                }
            });
            
            this.getParents(parentId);

            this.categoryList = newList;
        },
        getParents(id){
            this.parentIds = [];
            if(id <= 0) { return };

            var i = id;
            var array = []

            while (i > 0){
                var category = this.findCategory(i);
                i = category.parent.id;
                array.unshift(i);
            };

            this.parentIds = array;
        },
        getParentName(id){
            for (let i = 0; i < this.categories.length; i++) {
                if (this.categories[i].parent.id == id) {
                    return this.categories[i].parent.name;
                };
            }
        },
        findCategory(id){
            if (id == 0) { return }

            for (let i = 0; i < this.categories.length; i++) {
                if (this.categories[i].id == id) {
                    return this.categories[i];
                };
            }
        },

        newCategory() {
            this.categoryModel = {
                name: ""
            }
        },
        createCategory() {
            this.loading = true;

            var request = new FormData();
            request.append("name", this.categoryModel.name);
            request.append("parentId", this.categoryModel.parentId);

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
            
            this.getCategories();
            this.categoryModel = {
                id: null,
                name: "",
                parentId: null
            };
        },
        
        getCategory(id){
            var category = this.findCategory(id);

            this.categoryModel.id = category.id;
            this.categoryModel.name = category.name;
            this.categoryModel.parentId = category.parent.id;

            this.adding = false;
            this.updating = true;
        },
        updateCategory() {
            this.loading = true;

            var request = new FormData();
            request.append("id", this.categoryModel.id);
            request.append("name", this.categoryModel.name);
            request.append("parentId", this.categoryModel.parentId);

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
            
            this.updating = false;
            this.adding = true;
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

        },
    }
})