(function () {
    angular.module("app").controller("app.views.ccaca.user.modifyModal",
        function ($scope, $uibModalInstance, homeService, id) {
            var vm = this;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getUser(id).then(function (res) {
                    $scope.$apply(function () {
                        vm.model = res.result;
                    });
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.save = function () {
                abp.ui.setBusy();

                homeService.modifyUser({
                    id: vm.model.id,
                    userName: vm.model.userName,
                    realName: vm.model.realName,
                    password: vm.model.passWord,
                    email: vm.model.email,
                    isVip: vm.model.isVip,
                    phone: vm.model.phone
                }).then(function (res) {
                    if (res.result.state) {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    }
                });
            }

            getModel();
        });
})();