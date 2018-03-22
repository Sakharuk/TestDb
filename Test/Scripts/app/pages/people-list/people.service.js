(function (angular) {

    angular
        .module('testModule')
        .service('peopleService', peopleService);

    peopleService.$inject = ['$http'];

    function peopleService($http) {

        var service = {
            getPeoplePage: getPeoplePage,
            addPerson: addPerson
        };

        return service;

        function getPeoplePage(pagingInfo) {
            var promise = $http({
                url: '/Home/GetPeoplePage',
                method: 'GET',
                params: pagingInfo
            }).then(populateResponse);
            return promise;
        };

        function addPerson(person) {
            var request = $http({
                method: 'POST',
                url: '/Home/Create',
                data: JSON.stringify(person),
                dataType: "json"
            });
            return request;
        }  

        function populateResponse(response) {
            return response.data;
        };        
    };
})(angular);