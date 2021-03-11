var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        showList: true,
        showProject: false,
        objectIndex: 0,

        projects: [],
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

            this.toggleList();
        },
        getProject(id, index) {
            this.loading = true;
            this.objectIndex = index;

            axios.get('/Projects/' + id) 
                .then(res => {
                    this.projectModel = {
                        id: res.data.id,
                        title: res.data.title,
                        description: res.data.description,
                        tags: res.data.tags,
                        image: res.data.imagePath,
                    }
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
            
            this.toggleProject();
        },
        newProject() {
            this.projectModel = {
                id: "",
                title: "",
                description: "",
                tags: "",
                image: "",
            },

            this.toggleProject();
        },
        getFile(event) {
            this.imageFile = event.target.files[0];
        },
        createProject() {
            this.loading = true;

            var formData = new FormData();

            formData.append("title", this.projectModel.title);
            formData.append("description", this.projectModel.description);
            formData.append("tags", this.projectModel.tags);
            formData.append("image", this.imageFile);

            axios.post("/Projects/", formData,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(res => {
                    this.projects.push(res.data)
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.getProjects();
        },
        updateProject() {
            this.loading = true;

            var formData = new FormData();

            formData.append("id", this.projectModel.id);
            formData.append("title", this.projectModel.title);
            formData.append("description", this.projectModel.description);
            formData.append("tags", this.projectModel.tags);
            formData.append("imageFile", this.imageFile);
            formData.append("image", this.projectModel.image);

            axios.put("/Projects", formData,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(res => {
                    console.log(objectIndex);
                    console.log(res.data);
                    this.projects.splice(this.objectIndex, 1, res.data)
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.getProjects();
        },
        deleteProject(id) {
            this.loading = true;
            console.log(this.projectModel);

            axios.delete("/Projects/" + id + "/" + this.projectModel.image)
                .then(res => {
                    this.projects.splice(this.objectIndex, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

            this.getProjects();
        },

        toggleList() {
            this.showProject = false;
            this.showList = true;
        },

        toggleProject() {
            this.showList = false;
            this.showProject = true;
        }
    }
})