(function () {
    angular.module("app").controller("app.views.ccaca.contact.modifyModal",
        function ($scope, $uibModalInstance, homeService, id, lang) {
            var vm = this;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getModel() {
                homeService.getContact(id, lang).then(function (res) {
                    $scope.$apply(function () {
                        vm.model = res.result;
                        vm.model.lang = lang;
                    });
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            }

            vm.save = function () {
                abp.ui.setBusy();
                if (vm.model.lang == 1) {
                    homeService.modifyContact({
                        id: vm.model.id,
                        title: vm.model.title,
                        content: vm.model.content,
                        type: vm.model.type
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                            abp.ui.clearBusy();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_Configure",
                        id: vm.model.id,
                        languageId: vm.model.lang,
                        props: {
                            title: vm.model.title,
                            content: vm.model.content
                        }
                    }).then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    });
                }
            }

            getModel();
        });
})();