var app = new Vue({
    el: "#app",
    data: {
        loading: false,
        products: [],
        displayProducts: null,
        category: "",

        inputMinValue: "",
        inputMaxValue: "",

        sortType: '',
        sortOptions: [
            { text: 'Menor precio', value: 'min' },
            { text: 'Mayor precio', value: 'max' },
            { text: 'A-Z', value: 'abc'}
        ],

        filterOptions: [
            { text: ''}
        ]
    },
    created() {
        let urlParams = new URLSearchParams(window.location.search);
        if (urlParams.has('name')) {
            this.category = urlParams.get('name');
        }

        this.getProducts(this.category);
        this.getCategory(this.category);
    },
    methods: {
        getProducts(name) {
            this.loading = true;
            axios.get("/Store/vue/products/" + name)
                .then(res => {
                    this.products = res.data;
                    this.displayProducts = (res.data).sort((a, b) => a.name > b.name);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getCategory(name) {
            this.loading = true;
            axios.get("/Store/vue/categories/" + name)
                .then(res => {
                    this.category = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        // FILTER PRODUCTS

        maxValue(minValue, maxValue) {
            var prod = Array.from(this.products);

            this.displayProducts = prod.slice().filter(x => x.value >= minValue && x.value <= maxValue);
        },

        // SORT PRODUCTS

        sortBy(param) {
            if (param == "min") {
                this.displayProducts = this.displayProducts.sort((a, b) => a.value - b.value);
            }
            else if (param == "max") {
                this.displayProducts = this.displayProducts.sort((b, a) => a.value - b.value);
            } 
            else if (param == "abc") {
                this.displayProducts = this.displayProducts.sort((a, b) => a.name > b.name);
            }
        },

        // ------------------- //

        getCategoryUrl(name){
            return "/Store/Products?name=" + name;
        },
        getProductUrl(name){
            return "/Store/Product/" + name.replaceAll(" ", "-");
        },
    },
    computed: {
        
    }
})