//получаем элементы
function getToMapDataElement(authModel, name) {
    var mapData = authModel.MapData;

    for (var i = 0; i < mapData.length; i++) {
        var element = mapData[i];
        if (element.AreaName == name) return element.AreaShow;
    }
    return false;
}

//конфигурируем зоны видимости
function configureMapArea($scope, authModel) {

//    $scope.authData = {};
//    $scope.authData.companiesLinkShow = getToMapDataElement(authModel, "CompaniesLink");
//    $scope.authData.usersLinkShow = getToMapDataElement(authModel, "UsersLink");
//    $scope.authData.menuLinkShow = getToMapDataElement(authModel, "MenuLink");
//    $scope.authData.catalogLinkShow = getToMapDataElement(authModel, "CatalogLink");
//    $scope.authData.settingLinkShow = getToMapDataElement(authModel, "SettingLink");
//    $scope.authData.orderLinkShow = getToMapDataElement(authModel, "OrdersLink");
//    $scope.authData.chatLinkShow = getToMapDataElement(authModel, "ChatLink");
//    $scope.authData.modifLinkShow = getToMapDataElement(authModel, "ModificatorsLink");


}