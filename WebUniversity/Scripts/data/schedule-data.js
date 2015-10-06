$(document).ready(function () {

    var teachers;

    $('#teacher-block').hide();

    function getTeachersByCourse(id) {
        $('#teacher').empty();
        $.get('../../Schedules/GetTeachersByCourse', { Id: id }).done(function (data) {
            teachers = data;

            if (teachers.length > 0) {
                $('#teacher').append($('<option>').text('--Select--'));

                $.each(teachers, function (i, value) {
                    $('#teacher').append($('<option>').text(value.name).attr('value', value.id));
                });
            } else {
                $('#teacher').append($('<option>').text('No teacher'));
            }
        }).fail(function () {
            alert('Error!Please, try again!');
        });
    }

    $('select#course').change(function () {
        var id = $(this).val();

        $('#teacher-block').show();

        getTeachersByCourse(id);
    });
});