(function () {
    angular.module("app").controller("app.views.ccaca.user.createModal",
        function ($scope, $uibModalInstance, homeService, $timeout) {
            var vm = this;
            vm.model = {};

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            $timeout(function () {
                $scope.editor = homeService.createEditor();
            });

            vm.save = function () {
                abp.ui.setBusy();
                homeService.createUser({
                    userName: vm.model.userName,
                    realName: vm.model.realName,
                    password: vm.model.password,
                    isVip: vm.model.isVip,
                    email: vm.model.email,
                    phone: vm.model.phone
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