from google.appengine.api import users
import jinja2


jinja_environment = jinja2.Environment(
    loader=jinja2.FileSystemLoader('templates'))

def generate_from_template(file, template_values={}):
    """Generates output from a template with the supplied variables."""
    template = jinja_environment.get_template(file)
    output = template.render(template_values)
    return output


def render(handler, file, **kwargs):
    """Renders a template."""
    user = users.get_current_user()

    template_values = {
        'user': user,
        'user_is_admin': users.is_current_user_admin(),
    }

    if user:
        template_values.update({'login_url': users.create_logout_url('/')})
    else:
        template_values.update({'login_url': users.create_login_url('/games')})

    template_values.update(kwargs)
    handler.response.out.write(generate_from_template(file, template_values))


def render_error(handler, error):
    """Renders an error page."""
    valid_errors = [403, 404, 500]

    # If the error code given is not in the list, default to 404.
    if error not in valid_errors:
        error = 404

    # Set the error code on the handler
    handler.error(error)
    render(handler, 'error_%d.html' % error)
