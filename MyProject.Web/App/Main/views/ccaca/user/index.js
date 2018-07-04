(function () {
    angular.module('app').controller('app.views.ccaca.user.index',
        function ($scope, $uibModal, homeService) {
            var vm = this;

            vm.files = [];

            function getList() {
                homeService.getUserList().then(function (res) {
                    $scope.$apply(function () {
                        vm.models = res.result;
                    });
                });
            }

            vm.openCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/user/createModal.cshtml',
                    controller: 'app.views.ccaca.user.createModal as vm',
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
                    templateUrl: '/App/Main/views/ccaca/user/modifyModal.cshtml',
                    controller: 'app.views.ccaca.user.modifyModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return item.id;
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
                    "确定删除 " + item.userName + "?", "删除",
                    function (result) {
                        if (result) {
                            homeService.deleteUser(item.id)
                                .then(function () {
                                    abp.notify.info("Deleted tenant: " + item.userName);
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