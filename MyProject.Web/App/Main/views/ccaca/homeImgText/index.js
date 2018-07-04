(function () {
    angular.module('app').controller('app.views.ccaca.homeImgText.index',
        function ($scope, $uibModal, homeService) {
            var vm = this;

            vm.files = [];

            function getList() {
                homeService.homeList().then(function (res) {
                    $scope.$apply(function () {
                        vm.models = res.result;
                    });
                });
            }

            vm.openModifyModal = function (file, lang) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/homeImgText/modifyModal.cshtml',
                    controller: 'app.views.ccaca.homeImgText.modifyModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return file.id;
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

            vm.delete = function (file) {
                abp._message.confirm(
                    "确定删除 " + file.name + "?", "删除",
                    function (result) {
                        if (result) {
                            homeService.deleteFile(file.id)
                                .then(function () {
                                    abp.notify.info("Deleted tenant: " + file.name);
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