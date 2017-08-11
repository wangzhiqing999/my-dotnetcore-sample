(function () {
    $(function () {

        var _otherService = abp.services.app.other;
        var _$modal = $('#OtherCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshOtherList();
        });

        $('.delete-other').click(function () {
            var otherId = $(this).attr("data-other-id");
            var tenancyName = $(this).attr('data-tenancy-name');

            deleteOther(otherId, tenancyName);
        });

        $('.edit-other').click(function (e) {
            var otherId = $(this).attr("data-other-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Others/EditOtherModal?id=' + otherId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#OtherEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var other = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$modal);
            _otherService.create(other).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new other!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshOtherList() {
            location.reload(true); //reload page to see new other!
        }

        function deleteOther(otherId, tenancyName) {
            abp.message.confirm(
                "Delete other '" + tenancyName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _otherService.delete({
                            id: otherId
                        }).done(function () {
                            refreshOtherList();
                        });
                    }
                }
            );
        }
    });
})();