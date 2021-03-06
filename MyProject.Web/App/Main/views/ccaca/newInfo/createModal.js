﻿(function () {
    angular.module("app").controller("app.views.ccaca.newInfo.createModal",
        function ($scope, $uibModalInstance, homeService, $timeout, $filter) {
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
                homeService.createNewInfo({
                    title: vm.model.title,
                    file_conver: angular.element("#file_conver")[0].files[0],
                    content: vm.model.content,
                    createTime: $filter("date")(vm.model.createTime, "yyyy-MM-dd")
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