import React, { useState, useEffect } from 'react';
import axios from 'axios';

const GroupDetails = ({ routeId }) => {
    const [group, setGroup] = useState(null);
    const [users, setUsers] = useState([]);
    const [newUserEmail, setNewUserEmail] = useState('');

    useEffect(() => {
        if (routeId) {
            getGroupData();
        }
    }, [routeId]);

    const getGroupData = async () => {
        try {
            const response = await axios.get(`https://localhost:7160/api/Group/GetByRouteId`, {
                params: { routeId }
            });
            setGroup(response.data);
            getUsersInGroup(response.data.id)
        } catch (error) {
            console.error('Error fetching group data', error);
        }
    };

    const getUsersInGroup = async (groupId) => {
        try {
            const response = await axios.get(`https://localhost:7160/api/UserGroup/GetUsersFromGroup`, {
                params: { groupId }
            });
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching users in group', error);
        }
    };

    const addUserToGroup = async () => {
        try {
            const userResponse = await axios.get(`https://localhost:7160/api/Identity/GetIdByEmail`, {
                params: { email: newUserEmail }
            });
            const userId = userResponse.data;

            const userGroup = {
                Id: "asd",
                UserId: userId,
                GroupId: group.id 
            };

            const response = await axios.post(`https://localhost:7160/api/UserGroup/Add`, userGroup);

            getUsersInGroup(group.id);
        } catch (error) {
            console.error('Error adding user to group', error);
        }
    };

    const removeUserFromGroupGroup = async (id) => {
        try {
            const response = await axios.delete(`https://localhost:7160/api/UserGroup/Delete`, {
                params: { id }
            });
            setUsers(response.data);
        } catch (error) {
            console.error('Error deleting users in group', error);
        }
    };

    const handleAddUser = () => {
        addUserToGroup();
    };

    const handleRemoveUser = async (id) => {
        removeUserFromGroupGroup(id)
    };

    return (
        <div>
            {group ? (
                <>
                    <h3>Group Name: {group.name}</h3>
                </>
            ) : (
                <p>Loading group details...</p>
            )}

            <h4>Users in Group:</h4>
            {users.length > 0 ? (
                <ul>
                    {users.map((user) => (
                        <li key={user.id}>
                            {user.userName} - {user.email} - {user.userRole}
                            <button onClick={() => handleRemoveUser(user.id)}>Remove</button>
                        </li>
                    ))}
                </ul>
            ) : (
                <p>The group is empty.</p>
            )}

            <div>
                <h4>Add Traveler</h4>
                <input
                    type="email"
                    placeholder="Traveler's email"
                    value={newUserEmail}
                    onChange={(e) => setNewUserEmail(e.target.value)}
                />
                <button onClick={handleAddUser}>Submit</button>
            </div>
        </div>
    );
};

export default GroupDetails;