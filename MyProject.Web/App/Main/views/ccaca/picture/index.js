(function () {
    angular.module('app').controller('app.views.ccaca.picture.index',
        function ($scope, $timeout, $uibModal, $stateParams, homeService) {
            var vm = this;
            var langId = 1;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getHomeImg({ type: "picture", langId: langId }).then(function (res) {
                    $scope.$apply(function () {
                        vm.model = res.result;
                        vm.model.lang = langId;
                    })
                });
            }

            vm.refresh = function () {

            };

            vm.setLanguage = function () {
                langId = vm.model.lang;
                getModel();
            }

            vm.save = function () {
                abp.ui.setBusy();
                if (vm.model.lang == 1) {
                    homeService.modifyFile({
                        id: vm.model.id,
                        name: vm.model.name,
                        path: vm.model.path,
                        file: angular.element("#filepath")[0].files[0],
                        type: 'picture'
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            abp.ui.clearBusy();
                            getModel();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_Carousel",
                        id: vm.model.id,
                        languageId: vm.model.lang,
                        props: {
                            name: vm.model.name,
                            path: vm.model.path,
                            file_path: angular.element("#filepath")[0].files[0]
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