function ApplaySelect2(id, placehoderParam) {
    $(document).ready(function () {
        $(id).select2({
            placeholder: placehoderParam
        });
    });
}

ApplaySelect2("#CategoriesId", "Select Category")
ApplaySelect2("#AuthorsId", "Select Author")
ApplaySelect2("#PublisherId", "Select Publisher")
ApplaySelect2("#RolesList", "Select Role")