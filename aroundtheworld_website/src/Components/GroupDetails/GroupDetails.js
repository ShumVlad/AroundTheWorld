import React, { useState, useEffect, useContext } from 'react';
import axios from 'axios';
import { AuthContext } from '../../context/AuthContext';

const GroupDetails = ({ routeId }) => {
    const { authState } = useContext(AuthContext);
    const [group, setGroup] = useState(null);
    const [users, setUsers] = useState([]);
    const [newUserEmail, setNewUserEmail] = useState('');
    const [newGuideEmail, setNewGuideEmail] = useState('');

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
            getUsersInGroup(response.data.id);
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

    const addUserToGroup = async (email, role) => {
        try {
            const userResponse = await axios.get(`https://localhost:7160/api/Identity/GetIdByEmail`, {
                params: { email }
            });
            const userId = userResponse.data;

            const userGroup = {
                Id: "asd", // This should be a unique identifier, consider using a different approach for generating it.
                UserId: userId,
                GroupId: group.id,
                UserRole: role
            };

            await axios.post(`https://localhost:7160/api/UserGroup/Add`, userGroup);
            getUsersInGroup(group.id);
        } catch (error) {
            console.error('Error adding user to group', error);
        }
    };

    const handleAddUser = () => {
        addUserToGroup(newUserEmail, 'Traveler');
    };

    const handleAddGuide = () => {
        addUserToGroup(newGuideEmail, 'Guide');
    };

    const removeUserFromGroup = async (id) => {
        try {
            await axios.delete(`https://localhost:7160/api/UserGroup/Delete`, {
                params: { id }
            });
            getUsersInGroup(group.id);
        } catch (error) {
            console.error('Error removing user from group', error);
        }
    };

    const handleRemoveUser = async (id) => {
        removeUserFromGroup(id);
    };

    const guide = users.find(user => user.userRole === "Guide");
    const travelers = users.filter(user => user.userRole !== "Guide" && user.userRole !== "Worker");

    return (
        <div>
            {group ? (
                <>
                    <h2>{group.name}</h2>
                </>
            ) : (
                <p>Loading group details...</p>
            )}

            <h4>Guide:</h4>
            {guide ? (
                <div>
                    <p>{guide.userName} - {guide.email}</p>
                    {authState.userRole === "Guide" || authState.userRole === "Worker" ? (
                        <button onClick={() => handleRemoveUser(guide.id)}>Remove</button>
                    ) : null}
                </div>
            ) : (
                <>
                    <p>Guide will be added soon.</p>
                    {authState.userRole === "Guide" || authState.userRole === "Worker" ? (
                        <div>
                            <h4>Add Guide</h4>
                            <input
                                type="email"
                                placeholder="Guide's email"
                                value={newGuideEmail}
                                onChange={(e) => setNewGuideEmail(e.target.value)}
                            />
                            <button onClick={handleAddGuide}>Submit</button>
                        </div>
                    ) : null}
                </>
            )}

            <h4>Travelers:</h4>
            {travelers.length > 0 ? (
                <ul>
                    {travelers.map((user) => (
                        <li key={user.id}>
                            {user.userName} - {user.email}
                            {authState.userRole === "Guide" || authState.userRole === "Worker" ? (
                                <button onClick={() => handleRemoveUser(user.id)}>Remove</button>
                            ) : null}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No travelers in the group.</p>
            )}

            {authState.userRole === "Guide" || authState.userRole === "Worker" ? (
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
            ) : null}
        </div>
    );
};

export default GroupDetails;
