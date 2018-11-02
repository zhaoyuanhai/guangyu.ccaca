(function () {
    angular.module("app").factory("homeService", function () {
        $.ajaxSetup({
            error: function (res) {
                console.log(res);
            },
            beforeSend: function () {
                abp.ui.setBusy();
            },
            complete: function () {
                abp.ui.clearBusy();
            }
        })

        var service = {
            createEditor: function (id, _ops) {
                var options = {
                    cssPath: '/Content/kindeditor/plugins/code/prettify.css',
                    filterMode: true,
                    items: ['source',
                        'justifyleft', 'justifycenter', 'justifyright', '|',
                        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
                        'superscript', '|',
                        'forecolor', 'emoticons', 'fullscreen',
                        'textcolor', 'bgcolor', 'bold',
                        'italic', 'underline', 'strikethrough', 'removeformat', '|', 'image'],
                    width: "550px",
                    minWidth: "200px",
                    maxWidth: "550px",
                    uploadJson: '/Service/UpLoadFile',
                    allowFileManager: true
                };

                $.extend(options, _ops);

                return KindEditor.create(id || '#editor', options);
            },

            createFile: function (model) {
                var formData = new FormData();
                formData.append("name", model.name);
                formData.append("file", model.file);
                formData.append("type", model.type);

                return $.ajax({
                    url: "/Service/CreateFile",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST"
                });
            },

            modifyFile: function (model) {
                var formData = new FormData();
                formData.append("id", model.id);
                formData.append("type", model.type);
                formData.append("path", model.path);
                formData.append("name", model.name);
                formData.append("file", model.file);

                return $.ajax({
                    url: "/Service/modifyFile",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST"
                });
            },

            setRef: function (model) {
                var formData = new FormData();
                formData.append("tableName", model.tableName);
                formData.append("id", model.id);
                formData.append("languageId", model.languageId);
                for (var name in model.props) {
                    formData.append("props." + name, model.props[name]);
                }

                return $.ajax({
                    url: "/Service/SefRef",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST"
                });
            },

            getFiles: function (model) {
                return $.ajax({
                    url: "/Service/GetFiles",
                    type: "GET",
                    DataType: "JSON",
                    data: { type: model.type },
                });
            },

            getFile: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetFile",
                    type: "GET",
                    dataType: "JSON",
                    data: { id: id, langId: langId }
                });
            },

            deleteFile: function (id) {
                return $.ajax({
                    url: "/Service/DeleteFile",
                    data: { id: id },
                    type: "POST"
                });
            },

            homeList: function () {
                return $.ajax({
                    url: "/Service/HomeList",
                    type: "GET"
                });
            },

            getHome: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetHome",
                    type: "GET",
                    dataType: "JSON",
                    data: { id: id, langId: langId }
                });
            },

            modifyHome: function (model) {
                var formData = new FormData();
                formData.append("id", model.id);
                formData.append("title", model.title);
                formData.append("cover", model.cover)
                formData.append("file_cover", model.file_cover);
                formData.append("content", model.content);

                return $.ajax({
                    url: "/Service/ModifyHome",
                    data: formData,
                    type: "POST",
                    processData: false,
                    contentType: false
                });
            },

            getHomeImg: function (model) {
                return $.ajax({
                    url: "/Service/getHomeImg",
                    data: model,
                    type: "GET"
                })
            },

            getCcaca: function (model) {
                return $.ajax({
                    url: "/Service/GetCcaca",
                    data: model,
                    type: "GET"
                });
            },

            modifyCcaca: function (model) {
                var formData = new FormData();

                formData.append("id", model.id);
                formData.append("title", model.title);
                formData.append("content", model.content);
                formData.append("type", model.type);

                return $.ajax({
                    url: "/Service/ModifyCcaca",
                    data: model,
                    type: "POST"
                });
            },

            //member
            getMemberList: function () {
                return $.ajax({
                    url: "/Service/GetMemberList",
                    type: "GET"
                })
            },

            getMember: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetMember",
                    type: "GET",
                    data: { id: id, langId: langId }
                });
            },

            modifyMember: function (model) {
                return $.ajax({
                    url: "/Service/ModifyMember",
                    type: "POST",
                    data: model
                });
            },

            createMember: function (model) {
                return $.ajax({
                    url: "Service/CreateMember",
                    data: model,
                    type: "POST"
                });
            },

            deleteMember: function (id) {
                return $.ajax({
                    url: "/Service/DeleteMember",
                    data: { id: id },
                    type: "GET"
                });
            },

            //memberlink
            getMemberLinkList: function () {
                return $.ajax({
                    url: "/Service/GetMemberLinkList",
                    type: "GET"
                })
            },

            getMemberLink: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetMemberLink",
                    type: "GET",
                    data: { id: id, langId: langId }
                });
            },

            modifyMemberLink: function (model) {
                var formData = new FormData();
                formData.append("id", model.id);
                formData.append("name", model.name);
                formData.append("link", model.link);
                formData.append("icon", model.icon);
                formData.append("file_icon", model.file_icon);

                return $.ajax({
                    url: "/Service/ModifyMemberLink",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formData
                });
            },

            createMemberLink: function (model) {
                var formData = new FormData();
                formData.append("name", model.name);
                formData.append("link", model.link);
                formData.append("file_icon", model.file_icon);

                return $.ajax({
                    url: "/Service/CreateMemberLink",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formData
                });
            },

            deleteMemberLink: function (id) {
                return $.ajax({
                    url: "/Service/DeleteMemberLink",
                    data: { id: id },
                    type: "GET"
                });
            },

            //company
            getCompanyList: function () {
                return $.ajax({
                    url: "/Service/GetCompanyList",
                    type: "GET"
                })
            },

            getCompany: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetCompany",
                    type: "GET",
                    data: { id: id, langId: langId }
                })
            },

            modifyCompany: function (model) {
                var formData = new FormData();
                formData.append("id", model.id);
                formData.append("name", model.name);
                formData.append("link", model.link);
                formData.append("icon", model.icon);
                formData.append("file_icon", model.file_icon);

                return $.ajax({
                    url: "/Service/ModifyCompany",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formData
                });
            },

            createCompany: function (model) {
                var formData = new FormData();
                formData.append("name", model.name);
                formData.append("link", model.link);
                formData.append("file_icon", model.file_icon);

                return $.ajax({
                    url: "/Service/CreateCompany",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formData
                });
            },

            deleteCompany: function (id) {
                return $.ajax({
                    url: "/Service/DeleteCompany",
                    data: { id: id },
                    type: "GET"
                });
            },

            //newinfo
            getNewInfoList: function () {
                return $.ajax({
                    url: "/Service/GetNewInfoList",
                    type: "GET"
                });
            },

            getNewInfo: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetNewInfo",
                    type: "GET",
                    data: { id: id, langId: langId }
                });
            },

            createNewInfo: function (model) {
                var formData = new FormData();

                formData.append("title", model.title);
                formData.append("file_conver", model.file_conver);
                formData.append("content", model.content);

                return $.ajax({
                    url: "/Service/CreateNewInfo",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST"
                });
            },

            modifyNewInfo: function (model) {
                var formData = new FormData();

                formData.append("id", model.id);
                formData.append("title", model.title);
                formData.append("file_conver", model.file_conver);
                formData.append("content", model.content);
                formData.append("conver", model.conver);
                formData.append("createTime", model.createTime);

                return $.ajax({
                    url: "/Service/ModifyNewInfo",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST"
                });
            },

            deleteNewInfo: function (id) {
                return $.ajax({
                    url: "/Service/DeleteNewInfo",
                    data: { id: id },
                    type: "GET"
                })
            },

            //projectInfo
            getProjectInfoList: function () {
                return $.ajax({
                    url: "/Service/GetProjectInfoList",
                    type: "GET"
                })
            },

            getProjectInfo: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetProjectInfo",
                    data: { id: id, langId: langId },
                    type: "GET"
                });
            },

            createProjectInfo: function (model) {
                return $.ajax({
                    url: "/Service/CreateProjectInfo",
                    type: "POST",
                    data: model
                });
            },

            modifyProjectInfo: function (model) {
                return $.ajax({
                    url: "/Service/ModifyProjectInfo",
                    type: "POST",
                    data: model
                });
            },

            deleteProjectInfo: function (id) {
                return $.ajax({
                    url: "/Service/DeleteProjectInfo",
                    type: "GET",
                    data: { id: id }
                })
            },

            //contact
            getContactList: function () {
                return $.ajax({
                    url: "/Service/GetContactList",
                    type: "GET"
                });
            },

            getContact: function (id, langId) {
                return $.ajax({
                    url: "/Service/GetContact",
                    type: "GET",
                    data: { id: id, langId: langId }
                });
            },

            createContact: function (model) {
                return $.ajax({
                    url: "/Service/CreateContact",
                    type: "POST",
                    data: model
                });
            },

            modifyContact: function (model) {
                return $.ajax({
                    url: "/Service/ModifyContact",
                    type: "POST",
                    data: model
                });
            },

            deleteContact: function (id) {
                return $.ajax({
                    url: "/Service/DeleteContact",
                    type: "GET",
                    data: { id: id }
                });
            },

            //user
            getUserList: function () {
                return $.ajax({
                    url: "/Service/GetUserList",
                    type: "GET"
                });
            },

            getUser: function (id) {
                return $.ajax({
                    url: "/Service/GetUser",
                    type: "GET",
                    data: { id: id }
                });
            },

            modifyUser: function (model) {
                return $.ajax({
                    url: "/Service/ModifyUser",
                    type: "POST",
                    data: model
                });
            },

            createUser: function (model) {
                return $.ajax({
                    url: "/Service/CreateUser",
                    type: "POST",
                    data: model
                });
            },

            deleteUser: function (id) {
                return $.ajax({
                    url: "/Service/DeleteUser",
                    type: "GET",
                    data: { id: id }
                });
            },

            //dict
            getDictList: function () {
                return $.ajax({
                    url: "/Service/GetDictList",
                    type: "GET"
                });
            },

            setDict: function (model) {
                return $.ajax({
                    url: "/Service/SetDict",
                    data: model,
                    type: "POST"
                });
            },

            getDict: function (id) {
                return $.ajax({
                    url: "/Service/GetDict",
                    data: { id: id },
                    type: "POST"
                })
            },

            modifyDict: function (model) {
                return $.ajax({
                    url: "/Service/ModifyDict",
                    data: model,
                    type: "POST"
                })
            },

            deleteDict: function (word) {
                return $.ajax({
                    url: "/Service/DeleteDict",
                    data: { word: word },
                    type: "GET"
                })
            },

            //newActive
            getNewActiveList: function () {
                return $.ajax({
                    url: "/Service/GetNewActiveList",
                    type: "GET"
                })
            },

            createNewActive: function (model) {
                return $.ajax({
                    url: "/Service/CreateNewActive",
                    data: model,
                    type: "POST"
                })
            },

            modifyNewActive: function (model) {
                return $.ajax({
                    url: "/Service/ModifyNewActive",
                    data: model,
                    type: "POST"
                })
            },

            deleteNewActive: function (id) {
                return $.ajax({
                    url: "/Service/DeleteNewActive",
                    data: { id: id },
                    type: "GET"
                });
            },

            getNewActive: function (id) {
                return $.ajax({
                    url: "/Service/GetNewActive",
                    data: { id: id },
                    type: "GET"
                });
            },

            //fileMgr
            getFileMgrList: function () {
                return $.ajax({
                    url: "/Service/GetFileList",
                    type: "GET"
                });
            },

            createFileMgr: function (model) {
                return $.ajax({
                    url: "/Service/CreateFileMgr",
                    data: model,
                    type: "POST",
                    processData: false,
                    contentType: false
                });
            },

            deleteFileMgr: function (id) {
                return $.ajax({
                    url: "/Service/DeleteFileMgr",
                    data: { id: id },
                    type: "GET"
                });
            },

            getFileMgr: function (id) {
                return $.ajax({
                    url: "/Service/GetFileMgr",
                    data: { id: id },
                    type: "GET"
                });
            },

            //backimg
            backImg: function (model) {
                var formData = new FormData();
                formData.append("file", model.file);
                formData.append("module", model.module);

                return $.ajax({
                    url: "/Service/BackImg",
                    data: formData,
                    type: "POST",
                    processData: false,
                    contentType: false
                })
            }
        };

        return service;
    });
})();