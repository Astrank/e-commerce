var app = new Vue({
    el: "#app",
    data: {
        loading: false,
        products: [],
        displayProducts: null,

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
    mounted() {
        this.getProducts();
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get("/Store")
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

        dashed(name) {
            return name.replaceAll(" ", "-");
        }
    },
    computed: {
        
    }
})