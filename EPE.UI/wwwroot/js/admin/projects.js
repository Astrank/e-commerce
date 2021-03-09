var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        showList: true,
        showProject: false,

        projects: [],
        selectedProject: "",
        projectModel: {
            id: "",
            title: "",
            description: "",
            tags: "",
            image: "",
        },
        imageFile: "",
    },
    mounted() {
        this.getProjects();
    },
    methods: {
        getProjects() {
            this.loading = true;

            axios.get('/Projects')
                .then(res => {
                    this.projects = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProject(id) {
            this.loading = true,

            axios.get('/Projects' + id) 
                .then(res => {
                    this.selectedProject = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProject() {

        },
        getFile(event) {
            this.file = event.target.files[0];
        },
        editProject() {

        },

        toggleList() {
            this.showProject = false;
            this.showList = true;
        },

        toggleList() {
            this.showList = false;
            this.showProject = true;
        }
    }
})