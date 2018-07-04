(function () {
    angular.module("app").controller("app.views.ccaca.contact.createModal",
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
                homeService.createContact({
                    title: vm.model.title,
                    content: vm.model.content,
                    type: 2
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