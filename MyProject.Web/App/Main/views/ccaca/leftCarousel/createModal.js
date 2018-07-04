(function () {
    angular.module("app").controller("app.views.ccaca.leftCarousel.createModal",
        function ($scope, $uibModalInstance, homeService) {
            var vm = this;

            vm.file = {};

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.fileChange = function () {
                vm.file.path = "ddd";
            }

            vm.save = function () {
                abp.ui.setBusy();
                homeService.createFile({
                    name: vm.file.name,
                    file: angular.element("#filepath")[0].files[0],
                    type: "big"
                }).then(function (res) {
                    if (res.result.state) {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    }
                })
            }
        });
})();