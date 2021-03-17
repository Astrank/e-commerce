var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        isCategory: false,
        isSubcategory: false,
        showList: false,
        showElement: false,

        objectIndex: 0,

        projects: [],
        projectModel: {
            id: "",
            title: "",
            description: "",
            tags: "",
            primaryImage: "",
            images: null
        },

        primaryImageFile: null,
        imageFiles: null,
    },
    mounted() {
    },
    methods: {
        // CATEGORIES
        
        getCategories(){},
        getCategory(){},
        createCategory(){},
        updateCategory(){},
        deleteCategory(){},

        // SUBCATEGORIES

        getSubcategories(){},
        getSubcategy(){},
        createSubcategory(){},
        updateSubcategory(){},
        deleteSubcategory(){}
    }
})