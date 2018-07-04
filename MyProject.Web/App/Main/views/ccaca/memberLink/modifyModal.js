(function () {
    angular.module("app").controller("app.views.ccaca.memberLink.modifyModal",
        function ($scope, $uibModalInstance, homeService, id, lang) {
            var vm = this;
            vm.langs = [
                { id: 1, name: "中文" },
                { id: 2, name: "English" },
                { id: 3, name: "ESPANOL" },
                { id: 4, name: "PORTUGUES" }]

            function getFile() {
                homeService.getMemberLink(id, lang).then(function (res) {
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
                    homeService.modifyMemberLink({
                        id: vm.model.id,
                        name: vm.model.name,
                        link: vm.model.link,
                        icon: vm.model.icon,
                        file_icon: angular.element("#file_icon")[0].files[0]
                    }).then(function (res) {
                        if (res.result.state) {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                            abp.ui.clearBusy();
                        }
                    });
                } else {
                    homeService.setRef({
                        tableName: "T_MemberLink",
                        id: vm.model.id,
                        languageId: vm.model.lang,
                        props: {
                            name: vm.model.name,
                            link: vm.model.link,
                            icon: vm.model.icon,
                            file_icon: angular.element("#file_icon")[0].files[0]
                        }
                    }).then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                        abp.ui.clearBusy();
                    });
                }
            }

            getFile();
        });
})();