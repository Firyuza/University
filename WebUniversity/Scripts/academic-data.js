$(document).ready(function () {

    var groups,
        students,
        teachers;

    $('#course-block').hide();
    $('#teacher-block').hide();
    $('#student-block').hide();
    $('#score-block').hide();
    $('#date-block').hide();

    $.get('../../ExaminationDatasheets/GetGroups', function (data) {
        groups = data;
    }).done(function () {
        if (groups.length > 0) {
            $('#group').append($('<option>').text('--Select--'));

            $.each(groups, function (i, value) {
                $('#group').append($('<option>').text(value.Name).attr('value', value.GroupId));
            });
        } else {
            $('#group').append($('<option>').text('No group'));
        }

    }).fail(function () {
        alert('Error!Please, try again!');
    });

    function getStudentsByGroup(id) {
        $.get('GetStudentsByGroup', { Id: id }).done(function (data) {
            students = data;

            if (students.length > 0) {
                $('#student').append($('<option>').text('--Select--'));

                $.each(students, function (i, value) {
                    $('#student').append($('<option>').text(value.name).attr('value', value.id));
                });
            } else {
                $('#student').append($('<option>').text('No student'));
            }
        }).fail(function () {
            alert('Error!Please, try again!');
        });
    }

    function getTeachersByCourse(id) {
        $.get('GetTeachersByCourse', { Id: id }).done(function (data) {
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


    $('select#group').change(function () {
        var id = $(this).val();

        $('#student-block').show();

        $('#course-block').show();

        getStudentsByGroup(id);
    });

    $('select#course').change(function () {
        var id = $(this).val();

        $('#teacher-block').show();

        $('#score-block').show();

        $('#date-block').show();

        getTeachersByCourse(id);
    });
});