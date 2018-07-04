(function () {
    angular.module("app").controller("app.views.ccaca.dict.createModal",
        function ($scope, $uibModalInstance, homeService, $timeout) {
            var vm = this;
            vm.model = {};
            vm.langs = [
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            $timeout(function () {
                $scope.editor = homeService.createEditor();
            });

            vm.save = function () {
                abp.ui.setBusy();
                homeService.setDict({
                    word: vm.model.word,
                    value: vm.model.value,
                    languageId: vm.model.languageId
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