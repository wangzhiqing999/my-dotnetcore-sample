(function ($) {
    $(function () {

        // 刷新按钮点击.
        $('#RefreshButton').click(function () {
            location.reload(true);
        });


        $('.updateState').click(function () {
            var id = $(this).attr("data-id");
            var state = $(this).attr("data-state");

            if (state == "Open" || state == "0") {
                updateState(id, 1);
            } else {
                updateState(id, 0);
            }
        });

        function updateState(taskID, taskState) {
            var input = {
                TaskId: taskID,
                State: taskState
            };
            abp.services.app.task.updateTask(input)
                .done(function () {
                    location.href = '/Tasks';
                });
        }

    });
})(jQuery);