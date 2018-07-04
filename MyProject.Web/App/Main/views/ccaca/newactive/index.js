(function () {
    angular.module('app').controller('app.views.ccaca.newActive.index',
        function ($scope, $uibModal, homeService) {
            var vm = this;

            vm.files = [];

            function getList() {
                homeService.getNewActiveList().then(function (res) {
                    $scope.$apply(function () {
                        vm.models = res.result;
                    });
                });
            }

            vm.openCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/newActive/createModal.cshtml',
                    controller: 'app.views.ccaca.newActive.createModal as vm',
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
                    templateUrl: '/App/Main/views/ccaca/newActive/modifyModal.cshtml',
                    controller: 'app.views.ccaca.newActive.modifyModal as vm',
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
                    "确定删除 " + item.title + "?", "删除",
                    function (result) {
                        if (result) {
                            homeService.deleteNewActive(item.id)
                                .then(function () {
                                    abp.notify.info("Deleted tenant: " + item.title);
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