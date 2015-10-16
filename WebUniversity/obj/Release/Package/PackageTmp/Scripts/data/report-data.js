$(document).ready(function () {

    var groups,
        courses;

    $('#course-block').hide();

    function getCourseByGroup(id) {
        $('#course').empty();
        $.get('ExaminationDatasheets/GetCoursesByGroup', { Id: id }).done(function (data) {
            courses = _.uniq(data, function (item, key, a) {
                return item.Name;
            });

            if (courses.length > 0) {
                $('#course').append($('<option>').text('--Select--'));

                $.each(courses, function (i, value) {
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

    $('select#course').change(function () {
        var id = $(this).val();
    });
});