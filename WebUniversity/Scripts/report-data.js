$(document).ready(function () {

    var groups,
        courses,
        teachers;

    $('#course-block').hide();
    $('#teacher-block').hide();

    function getCourseByGroup(id) {
        $.get('ExaminationDatasheets/GetCoursesByGroup', { Id: id }).done(function (data) {
            courses = data;
            if (courses.length > 0) {
                $('#course').append($('<option>').text('--Select--'));

                $.each(groups, function (i, value) {
                    $('#course').append($('<option>').text(value.Name).attr('value', value.CourseId));
                });
            } else {
                $('#course').append($('<option>').text('No course'));
            }
        }).fail(function () {
            alert('Error!Please, try again!');
        });
    }

    $.get('ExaminationDatasheets/GetGroups', function(data) {
        groups = data;
    }).done(function() {
        if (groups.length > 0) {
            $('#group').append($('<option>').text('--Select--'));

            $.each(groups, function(i, value) {
                $('#group').append($('<option>').text(value.Name).attr('value', value.GroupId));
            });
        } else {
            $('#group').append($('<option>').text('No group'));
        }

    }).fail(function() {
        alert('Error!Please, try again!');
    });

    
    $('select#group').change(function () {
        var id = $(this).val();

        $('#course-block').show();

        getCourseByGroup(id);
    });
});