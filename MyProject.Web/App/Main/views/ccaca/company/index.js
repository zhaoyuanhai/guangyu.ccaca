(function () {
    angular.module('app').controller('app.views.ccaca.company.index',
        function ($scope, $uibModal, homeService) {
            var vm = this;

            vm.files = [];

            function getList() {
                homeService.getCompanyList().then(function (res) {
                    $scope.$apply(function () {
                        vm.models = res.result;
                    });
                });
            }

            vm.openCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/company/createModal.cshtml',
                    controller: 'app.views.ccaca.company.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getList();
                });
            };

            vm.openModifyModal = function (item, lang) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/company/modifyModal.cshtml',
                    controller: 'app.views.ccaca.company.modifyModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return item.id;
                        },
                        lang: function () {
                            return lang || 1;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getList();
                });
            }

            vm.delete = function (item) {
                abp._message.confirm(
                    "确定删除 " + item.name + "?", "删除",
                    function (result) {
                        if (result) {
                            homeService.deleteCompany(item.id)
                                .then(function () {
                                    abp.notify.info("Deleted tenant: " + item.name);
                                    getList();
                                });
                        }
                    });
            }

            vm.setLanguage = function (model, lang) {
                vm.openModifyModal(model, lang.id);
            }

            getList();
        });
})();