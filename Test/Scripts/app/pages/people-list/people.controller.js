(function (angular) {

    angular
        .module('testModule')
        .controller('PeopleController', PeopleController);

    PeopleController.$inject = ['$scope', 'peopleService'];

    function PeopleController($scope, peopleService, $uibModal, $uibModalInstance) {
        var vm = this;

        //fields
        vm.people = [];

        //paging with sorting 
        vm.pagingInfo = {
            currentPage: 1,
            itemsPerPage: 10,
            predicate: 'Id',
            reverse: false,
            peopleTotal: 0
        };

        //functions
        vm.reloadPeople = reloadPeople;
        vm.sortBy = sortBy;
        vm.showOrderArrow = showOrderArrow;
        vm.getNumber = getNumber;

        activate();

        function activate() {
            reloadPeople();
        };

        function reloadPeople() {
            var peoplePromise = peopleService.getPeoplePage(vm.pagingInfo);
            peoplePromise.then(function (data) {
                vm.people = data.People;
                vm.pagingInfo.peopleTotal = data.PeopleTotal;
            }, errorCallBack);
        };

        function sortBy(predicate) {
            vm.pagingInfo.reverse = (vm.pagingInfo.predicate === predicate) ? !vm.pagingInfo.reverse : false;
            vm.pagingInfo.predicate = predicate;
            reloadPeople();
        };

        function showOrderArrow(predicate) {
            if (vm.pagingInfo.predicate === predicate) {
                return vm.pagingInfo.reverse ? '▲' : '▼';
            }
            return '';
        };

        function getNumber(num) {
            num = Math.ceil(vm.pagingInfo.peopleTotal / num);
            return new Array(num);
        };

        function errorCallBack(error) {
            console.log('An unexpected error has occured: ' + error.statusText);
        };

        $scope.SavePerson = function () {
            var Person = {
                Id: $scope.id,
                FirstName: $scope.firstName,
                LastName: $scope.lastName,
                Value: $scope.value
            };
            
            var requestResponse = peopleService.addPerson(Person);
            message(requestResponse);
        }; 

        function message(requestResponse) {
            requestResponse.then(function (data) {
                $scope.message = '';
                $scope.errors = [];
                if (data.data.success == false) {
                    $scope.message = 'Validation Failed!';
                    $scope.errors = data.data.errors;
                }
                else {
                    $scope.person = {};
                    $('#addPerson').modal('hide');
                    cleanData();
                    reloadPeople();
                }
            }, function () {
                $('#addPerson').modal('hide');
                cleanData();
                reloadPeople();
            });
        }  

        $scope.Close = function () {
            $('#addPerson').modal('hide');
            cleanData();
            reloadPeople();
        }; 

        function cleanData()
        {
            $scope.message = '';
            $scope.errors = [];
            $scope.firstName = '';
            $scope.lastName = '';
            $scope.value = '';
        }
    }
})(angular);