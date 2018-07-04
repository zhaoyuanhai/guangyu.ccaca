(function () {
    angular.module("app").controller("app.views.ccaca.member.createModal",
        function ($scope, $uibModalInstance, homeService) {
            var vm = this;

            vm.file = {};

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.save = function () {
                abp.ui.setBusy();
                homeService.createMember({
                    item: vm.model.item,
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