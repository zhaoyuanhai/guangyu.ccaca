(function () {
    angular.module("app").controller("app.views.ccaca.leftCarousel.modifyModal",
        function ($scope, $uibModalInstance, homeService, id, lang) {
            var vm = this;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getFile() {
                homeService.getFile(id, lang).then(function (res) {
                    $scope.$apply(function () {
                        vm.file = res.result;
                        vm.file.lang = lang;
                    });
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.save = function () {
                abp.ui.setBusy();
                if (vm.file.lang == 1) {
                    homeService.modifyFile({
                        id: vm.file.id,
                        name: vm.file.name,
                        path: vm.file.path,
                        file: angular.element("#filepath")[0].files[0],
                        type: "big"
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                            abp.ui.clearBusy();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_Carousel",
                        id: vm.file.id,
                        languageId: vm.file.lang,
                        props: {
                            name: vm.file.name,
                            path: vm.file.path,
                            file_path: angular.element("#filepath")[0].files[0]
                        }
                    }).then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    });
                }
            }

            getFile();
        });
})();