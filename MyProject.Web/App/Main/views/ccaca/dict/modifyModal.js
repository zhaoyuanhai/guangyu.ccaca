(function () {
    angular.module("app").controller("app.views.ccaca.dict.modifyModal",
        function ($scope, $uibModalInstance, homeService, id) {
            var vm = this;
            vm.langs = [
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getDict(id).then(function (res) {
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

                homeService.modifyDict({
                    word: vm.model.word,
                    value: vm.model.value,
                    languageId: vm.model.languageId
                }).then(function () {
                    abp.notify.info(App.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                    abp.ui.clearBusy();
                });
                
            }

            getModel();
        });
})();