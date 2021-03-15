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
            primaryImage: "",
            images: null
        },

        primaryImageFile: null,
        imageFiles: null,
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
                    console.log(res);
                    this.projectModel = {
                        id: res.data.id,
                        title: res.data.title,
                        description: res.data.description,
                        tags: res.data.tags,
                        primaryImage: res.data.primaryImage,
                        images: res.data.images
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
        getPrimaryImage(event) {
            this.primaryImageFile = event.target.files[0];
        },
        getImageFiles(event) {
            this.imageFiles = event.target.files;
        },
        createProject() {
            this.loading = true;

            var formData = new FormData();

            formData.append("title", this.projectModel.title);
            formData.append("description", this.projectModel.description);
            formData.append("tags", this.projectModel.tags);
            formData.append("primaryImageFile", this.primaryImageFile);
            if (this.imageFiles != null) {
                for (let i = 0; i < this.imageFiles.length; i++) {
                    formData.append("imageFiles", this.imageFiles[i]);
                }
            };

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
            this.primaryImageFile = null;
            this.imageFiles = null;
        },
        updateProject() {
            this.loading = true;

            var formData = new FormData();

            formData.append("id", this.projectModel.id);
            formData.append("title", this.projectModel.title);
            formData.append("description", this.projectModel.description);
            formData.append("tags", this.projectModel.tags);
            formData.append("primaryImage", this.projectModel.primaryImage);
            formData.append("primaryImageFile", this.primaryImageFile);

            if (this.projectModel.images != null) {
                for (let i = 0; i < this.projectModel.images.length; i++) {
                    formData.append("images", this.projectModel.images[i]);
                }
            }
            
            if (this.imageFiles) {
                for (let i = 0; i < this.imageFiles.length; i++) {
                    formData.append("imageFiles", this.imageFiles[i]);
                }
            }

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
            this.primaryImageFile = null;
            this.imageFiles = null;
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