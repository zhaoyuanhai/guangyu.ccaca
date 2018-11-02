(function () {
    angular.module("app").controller("app.views.ccaca.newInfo.modifyModal",
        function ($scope, $uibModalInstance, homeService, id, lang, $filter) {
            var vm = this;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getNewInfo(id, lang).then(function (res) {
                    $scope.$apply(function () {
                        res.result.createTime = new Date(res.result.createTime);
                        vm.model = res.result;
                        vm.model.lang = lang;
                        $scope.editor = homeService.createEditor();
                        $scope.editor.html(vm.model.content);
                    });
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            vm.save = function () {
                vm.model.content = $scope.editor.html();
                abp.ui.setBusy();
                if (vm.model.lang == 1) {
                    homeService.modifyNewInfo({
                        id: vm.model.id,
                        title: vm.model.title,
                        content: vm.model.content,
                        conver: vm.model.conver,
                        file_conver: angular.element("#file_conver")[0].files[0],
                        createTime: $filter("date")(vm.model.createTime, "yyyy-MM-dd")
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                            abp.ui.clearBusy();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_Information",
                        id: vm.model.id,
                        languageId: vm.model.lang,
                        props: {
                            title: vm.model.title,
                            content: vm.model.content,
                            conver: vm.model.conver,
                            file_conver: angular.element("#file_conver")[0].files[0]
                        }
                    }).then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    });
                }
            };

            getModel();
        });
})();