(function () {
    angular.module('app').controller('app.views.ccaca.dict.index',
        function ($scope, $uibModal, homeService) {
            var vm = this;

            vm.files = [];

            function getList() {
                homeService.getDictList().then(function (res) {
                    $scope.$apply(function () {
                        vm.models = res.result;
                    });
                });
            }

            vm.openCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/dict/createModal.cshtml',
                    controller: 'app.views.ccaca.dict.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getList();
                });
            };

            vm.openModifyModal = function (item) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/dict/modifyModal.cshtml',
                    controller: 'app.views.ccaca.dict.modifyModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return item.id
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
                    "确定删除 " + item.word + "?", "删除",
                    function (result) {
                        if (result) {
                            homeService.deleteDict(item.word)
                                .then(function () {
                                    abp.notify.info("Deleted tenant: " + item.word);
                                    getList();
                                });
                        }
                    });
            }

            vm.setLanguage = function (model) {
                vm.openModifyModal(model);
            }

            getList();
        });
})();