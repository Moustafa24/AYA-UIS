import React, { useEffect, useState } from 'react';
import { FiBook } from 'react-icons/fi';
import courseService from '../../services/courseService';
import { useAuth } from '../../contexts/AuthContext';
import { toast } from 'react-toastify';

export default function DepartmentCourses() {
  const { user } = useAuth();
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);

  const departmentId = user?.departmentId;

  useEffect(() => {
    const load = async () => {
      if (!departmentId) {
        setLoading(false);
        return;
      }
      try {
        const res = await courseService.getDeptCourses(departmentId);
        setCourses(res?.data || res || []);
      } catch (e) {
        toast.error('Failed to load courses');
      }
      setLoading(false);
    };
    load();
  }, [departmentId]);

  if (loading)
    return (
      <div className="page-container">
        <div className="spinner" />
      </div>
    );

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>
          <FiBook style={{ marginRight: 8 }} />
          Department Courses
        </h1>
        <p>
          Courses available in{' '}
          {user?.departmentName ? (
            <span className="badge badge-info" style={{ marginLeft: 4 }}>
              {user.departmentName}
            </span>
          ) : (
            'your department'
          )}
        </p>
      </div>

      {!departmentId ? (
        <div className="card empty-state">
          <h3>No department assigned</h3>
          <p>
            Your account is not linked to a department. Contact your
            administrator.
          </p>
        </div>
      ) : courses.length === 0 ? (
        <div className="card empty-state">
          <h3>No courses found</h3>
          <p>No courses are available for your department yet.</p>
        </div>
      ) : (
        <div className="card">
          <div
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              marginBottom: 16,
            }}
          >
            <h3>All Courses</h3>
            <span className="badge badge-info">
              {courses.length} course{courses.length !== 1 ? 's' : ''}
            </span>
          </div>
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>Code</th>
                  <th>Name</th>
                  <th>Credits</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {courses.map(c => (
                  <tr key={c.id}>
                    <td>
                      <strong>{c.code}</strong>
                    </td>
                    <td>{c.name}</td>
                    <td>{c.credits}</td>
                    <td>
                      <span
                        className={`badge ${
                          c.status === 'Opened'
                            ? 'badge-success'
                            : 'badge-neutral'
                        }`}
                      >
                        {c.status}
                      </span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
}
