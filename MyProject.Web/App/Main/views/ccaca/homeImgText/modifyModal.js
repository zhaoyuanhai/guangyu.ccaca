(function () {
    angular.module("app").controller("app.views.ccaca.homeImgText.modifyModal",
        function ($scope, $uibModalInstance, homeService, $timeout, id, lang) {
            var vm = this;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getHome(id, lang).then(function (res) {
                    $scope.$apply(function () {
                        vm.model = res.result;
                        vm.model.lang = lang;
                        $scope.editor = homeService.createEditor();
                        $scope.editor.html(vm.model.content);
                    });
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.save = function () {
                abp.ui.setBusy();
                vm.model.content = $scope.editor.html();
                if (vm.model.lang == 1) {
                    homeService.modifyHome({
                        id: vm.model.id,
                        title: vm.model.title,
                        content: vm.model.content,
                        cover: vm.model.cover,
                        file_cover: angular.element("#filecover")[0].files[0],
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                            abp.ui.clearBusy();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_Home",
                        id: vm.model.id,
                        languageId: vm.model.lang,
                        props: {
                            title: vm.model.title,
                            content: vm.model.content,
                            cover: vm.model.cover,
                            file_cover: angular.element("#filecover")[0].files[0]
                        }
                    }).then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    });
                }
            }

            getModel();
        });
})();