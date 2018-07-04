(function () {
    angular.module("app").controller("app.views.ccaca.ccaca.index",
        function ($scope, homeService, $stateParams) {
            var vm = this;
            var langId = 1;
            var editor;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getCcaca({ id: $stateParams.id, langId: langId }).then(function (res) {
                    $scope.$apply(function () {
                        vm.model = res.result;
                        vm.model.lang = langId;
                        if (!editor)
                            editor = homeService.createEditor(null, { width: "100%" });
                        editor.html(vm.model.content);
                    });
                });
            }

            vm.setLanguage = function () {
                langId = vm.model.lang;
                getModel();
            }

            vm.save = function () {
                vm.model.content = editor.html();
                abp.ui.setBusy();
                if (vm.model.lang == 1) {
                    homeService.modifyCcaca({
                        id: vm.model.id,
                        title: vm.model.title,
                        content: vm.model.content,
                        type: vm.model.type
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            abp.ui.clearBusy();
                            getModel();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_Configure",
                        id: vm.model.id,
                        languageId: vm.model.lang,
                        props: {
                            title: vm.model.title,
                            content: vm.model.content
                        }
                    }).then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        abp.ui.clearBusy();
                        getModel();
                    });
                }
            }

            getModel();
        });
})();