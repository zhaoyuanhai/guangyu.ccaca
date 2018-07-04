(function () {
    angular.module("app").controller("app.views.ccaca.company.createModal",
        function ($scope, $uibModalInstance, homeService) {
            var vm = this;

            vm.file = {};

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.save = function () {
                abp.ui.setBusy();
                homeService.createCompany({
                    name: vm.model.name,
                    link: vm.model.link,
                    file_icon: angular.element("#file_icon")[0].files[0]
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