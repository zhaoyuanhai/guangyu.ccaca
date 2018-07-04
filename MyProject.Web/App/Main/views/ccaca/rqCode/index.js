(function () {
    angular.module('app').controller('app.views.ccaca.rqCode.index',
        function ($scope, $timeout, $uibModal, $stateParams, homeService) {
            var vm = this;
            var langId = 1;

            function getModel() {
                homeService.getHomeImg({ type: 'rqcode', langId: langId }).then(function (res) {
                    $scope.$apply(function () {
                        vm.model = res.result;
                    });
                });
            }

            vm.refresh = function () {

            };

            vm.setLanguage = function () {
                getModel();
            }

            vm.save = function () {
                abp.ui.setBusy();

                homeService.modifyFile({
                    id: vm.model.id,
                    path: vm.model.path,
                    file: angular.element("#filepath")[0].files[0],
                    type: 'rqcode'
                }).then(function (res) {
                    if (res.result.state) {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        abp.ui.clearBusy();
                        getModel();
                    }
                });

            }

            getModel();
        });
})();