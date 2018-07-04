(function () {
    angular.module('app').controller('app.views.ccaca.leftCarousel.index',
        function ($scope, $uibModal, homeService) {
            var vm = this;

            vm.files = [];

            function getFiles() {
                homeService.getFiles({ type: "big" }).then(function (res) {
                    $scope.$apply(function () {
                        vm.models = res.result;
                    });
                });
            }

            vm.openCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/leftCarousel/createModal.cshtml',
                    controller: 'app.views.ccaca.leftCarousel.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getFiles();
                });
            };

            vm.openModifyModal = function (file, lang) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/ccaca/leftCarousel/modifyModal.cshtml',
                    controller: 'app.views.ccaca.leftCarousel.modifyModal as vm',
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
                    getFiles();
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
                                    getFiles();
                                });
                        }
                    });
            }

            vm.setLanguage = function (file, lang) {
                vm.openModifyModal(file, lang.id);
            }

            getFiles();
        });
})();