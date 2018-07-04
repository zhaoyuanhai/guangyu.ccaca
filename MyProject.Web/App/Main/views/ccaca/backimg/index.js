angular.module('app').controller('app.views.ccaca.backimg.index',
    function ($scope, $timeout, $uibModal, $stateParams, homeService) {
        var vm = this;
        vm.model={
            module:"CCACA"
        };

        vm.getModel=function() {
            vm.model.path = "/BackImg/"+vm.model.module+".jpg?r="+Math.random();
        }

        vm.save = function () {
            abp.ui.setBusy();
              
            homeService.backImg({
                file: angular.element("#filepath")[0].files[0],
                module: vm.model.module
            }).then(function (res) {
                if (res.result.state) {
                    abp.notify.info(App.localize('SavedSuccessfully'));
                    abp.ui.clearBusy();
                    $scope.$apply(function(){
                        vm.getModel();
                    });
                }
            });
        }

        vm.getModel();
    });
