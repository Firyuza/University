$(document).ready(function () {

    var groups,
        students,
        courses;

    $('#course-block').hide();
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
        $('#student').empty();
        $.get('../../AcademicProgresses/GetStudentsByGroup', { Id: id }).done(function (data) {
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

    function getCoursesByGroup(id) {
        $('#course').empty();
        $.get('../../AcademicProgresses/GetCoursesByGroup', { Id: id }).done(function (data) {
            courses = _.uniq(data, function (item, key, a) {
                return item.name;
            });

            if (courses.length > 0) {
                $('#course').append($('<option>').text('--Select--'));

                $.each(courses, function (i, value) {
                    $('#course').append($('<option>').text(value.name).attr('value', value.id));
                });
            } else {
                $('#course').append($('<option>').text('No course'));
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

        getCoursesByGroup(id);
    });

    $('select#course').change(function () {
        var id = $(this).val();

        $('#score-block').show();

        $('#date-block').show();
    });
});