Vue.component('treeselect', VueTreeselect.Treeselect);    
    
var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        adding: true,
        updating: false,
        
        parentIds: [],

        objectIndex: 0,

        categories: [],
        categoryList: [],

        categoryModel: {
            id: null,
            name: "",
            parentId: null
        },

        // vue-treeselect
        value: null,
        options: [],
    },
    mounted() {
        this.getCategories();
    },
    methods: {
        buildTreeselect() {
            for (let i = 0; i < this.categories.length; i++) {
                // Adds main categories
                if (this.categories[i].parent.id == 0) {
                    this.options.push({
                        id: this.categories[i].id,
                        label: this.categories[i].name
                    });
                } 
                // Loops through categories to find its parent
                else {
                    this.options.forEach(category => {
                        this.findParent(category, this.categories[i]);
                    });
                }

            }
        },
        findParent(category, subcategory){
            if(category.id == subcategory.parent.id) {
                if (category.children == null) {
                    category.children = [{
                        id: subcategory.id,
                        label: subcategory.name
                    }]
                } else{
                    category.children.push({
                        id: subcategory.id,
                        label: subcategory.name
                    })
                }
            }
            else if(category.children != null){
                category.children.forEach(child => {
                    this.findParent(child, subcategory);
                });
            }
        },
        getCategories() {
            this.loading = true;

            axios.get("/Categories")
                .then(res => {
                    this.categories = res.data.sort(function (a, b) { return a.parent.id - b.parent.id });
                    this.buildTreeselect();
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
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
            request.append("parentId", this.value);

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
            
            this.categoryModel = {
                id: null,
                name: "",
            };
            this.value = null;
        },
        
        getCategory(id){
            var category = this.findCategory(id);

            this.categoryModel.id = category.id;
            this.categoryModel.name = category.name;
            this.categoryModel.parentId = category.parent.id;
            if(category.parent.id) {
                this.value = category.parent.id;
            } else {
                this.value = null;
            }

            this.adding = false;
            this.updating = true;
        },
        updateCategory() {
            this.loading = true;

            var request = new FormData();
            request.append("id", this.categoryModel.id);
            request.append("name", this.categoryModel.name);
            request.append("parentId", this.value);

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
        cancel() {
            this.categoryModel.id = null;
            this.categoryModel.name = "";
            this.categoryModel.parentId = null;
            this.value = null;

            this.updating = false;
            this.adding = true;
        }
    }
})