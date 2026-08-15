namespace Ayllu.Domain.Entities.Groups;

public enum GroupDialecticVisibility
{
    Private = 0,            // Apenas o dono
    Default = Private,      // Apenas o dono   
    GroupMembers = 1,       // Membros do grupo
    GroupAdmins = 2,        // Administradores do grupo
    Public = 4              // Todos podem ver
}