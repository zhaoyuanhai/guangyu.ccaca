﻿(function () {
    angular.module("app").controller("app.views.ccaca.newActive.createModal",
        function ($scope, $uibModalInstance, homeService, $timeout) {
            var vm = this;
            vm.model = {
                createTime: new Date()
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            $timeout(function () {
                $scope.editor = homeService.createEditor();
            });

            vm.save = function () {
                vm.model.content = $scope.editor.html();

                abp.ui.setBusy();
                homeService.createNewActive({
                    title: vm.model.title,
                    content: vm.model.content,
                    createTime: vm.model.createTime
                }).then(function (res) {
                    if (res.result.state) {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    }
                });
            };
        });
})();