(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
            var x = abp;
            $locationProvider.hashPrefix('');
            $urlRouterProvider.otherwise('/');
            $qProvider.errorOnUnhandledRejections(false);

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Main/views/users/index.cshtml',
                        menu: 'Users' //Matches to name of 'Users' menu in MyProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/users');
            }

            if (abp.auth.hasPermission('Pages.Roles')) {
                $stateProvider
                    .state('roles', {
                        url: '/roles',
                        templateUrl: '/App/Main/views/roles/index.cshtml',
                        menu: 'Roles' //Matches to name of 'Tenants' menu in MyProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/roles');
            }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Main/views/tenants/index.cshtml',
                        menu: 'Tenants' //Matches to name of 'Tenants' menu in MyProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/tenants');
            }

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in MyProjectNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in MyProjectNavigationProvider
                })
                .state("leftCarousel", {
                    url: "/leftCarousel",
                    templateUrl: "/App/Main/views/ccaca/leftCarousel/index.cshtml",
                    menu: "LeftCarousel"
                })
                .state("centerCarousel", {
                    url: "/centerCarousel",
                    templateUrl: "/App/Main/views/ccaca/centerCarousel/index.cshtml",
                    menu: "CenterCarousel"
                })
                .state("homeImgText", {
                    url: "/homeImgText",
                    templateUrl: "/App/Main/views/ccaca/homeImgText/index.cshtml",
                    menu: "HomeImgText"
                })
                .state("homeImage", {
                    url: "/homeImage?type",
                    templateUrl: "/App/Main/views/ccaca/homeImage/index.cshtml",
                    menu: "HomeImage"
                })
                .state("ccaca", {
                    url: "/ccaca?id",
                    templateUrl: "/App/Main/views/ccaca/ccaca/index.cshtml",
                    menu: "Ccaca"
                })
                .state("member", {
                    url: "/member",
                    templateUrl: "/App/Main/views/ccaca/member/index.cshtml",
                    menu: "Member"
                })
                .state("memberLink", {
                    url: "/memberLink",
                    templateUrl: "/App/Main/views/ccaca/memberLink/index.cshtml",
                    menu: "MemberLink"
                })
                .state("picture", {
                    url: "/picture",
                    templateUrl: "/App/Main/views/ccaca/picture/index.cshtml",
                    menu: "picture"
                })
                .state("projectInfo", {
                    url: "/projectInfo",
                    templateUrl: "/App/Main/views/ccaca/projectInfo/index.cshtml",
                    menu: "ProjectInfo"
                })
                .state("contact", {
                    url: "/contact",
                    templateUrl: "/App/Main/views/ccaca/contact/index.cshtml",
                    menu: "Contact"
                })
                .state("newInfo", {
                    url: "/newInfo",
                    templateUrl: "/App/Main/views/ccaca/newInfo/index.cshtml",
                    menu: "NewInfo"
                })
                .state("user", {
                    url: "/user",
                    templateUrl: "/App/Main/views/ccaca/user/index.cshtml",
                    menu: "User"
                })
                .state("rqCode", {
                    url: "/rqCode",
                    templateUrl: "/App/Main/views/ccaca/rqCode/index.cshtml",
                    menu: "RqCode"
                })
                .state("dict", {
                    url: "/dict",
                    templateUrl: "/App/Main/views/ccaca/dict/index.cshtml",
                    menu: "dict"
                })
                .state("newActive", {
                    url: "/newActive",
                    templateUrl: "/App/Main/views/ccaca/newactive/index.cshtml",
                    menu: "newActive"
                })
                .state("fileMgr", {
                    url: "/fileMgr",
                    templateUrl: "/App/Main/views/ccaca/filemgr/index.cshtml",
                    menu: "fileMgr"
                })
                .state("company", {
                    url: "/company",
                    templateUrl: "/App/Main/views/ccaca/company/index.cshtml",
                    menu: "company"
                })
                .state("backimg", {
                    url: "/backimg",
                    templateUrl: "/App/Main/views/ccaca/backimg/index.cshtml",
                    menu: "backimg"
                });
        }
    ]);

})();