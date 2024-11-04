using Application.User.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public record class UserUseCases(
        AddUser addUser
        );
}
