import React, { useEffect, useState } from 'react';
import { FiClipboard, FiPlus, FiTrash2 } from 'react-icons/fi';
import registrationService from '../../services/registrationService';
import courseService from '../../services/courseService';
import { useAuth } from '../../contexts/AuthContext';
import { toast } from 'react-toastify';

export default function RegisterCourses() {
  const { user } = useAuth();
  const [deptCourses, setDeptCourses] = useState([]);
  const [myCourses, setMyCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [form, setForm] = useState({
    courseId: '',
    studyYearId: '',
    semesterId: '',
  });

  useEffect(() => {
    const load = async () => {
      try {
        const [myRes] = await Promise.all([
          registrationService.getMyCourses().catch(() => []),
        ]);
        setMyCourses(myRes?.data || myRes || []);
      } catch (e) {}
      setLoading(false);
    };
    load();
  }, []);

  const loadDeptCourses = async deptId => {
    try {
      const res = await courseService.getDeptCourses(deptId);
      setDeptCourses(res?.data || res || []);
    } catch (e) {
      toast.error('Failed to load department courses');
    }
  };

  const handleRegister = async e => {
    e.preventDefault();
    try {
      await registrationService.register({
        courseId: parseInt(form.courseId),
        studyYearId: parseInt(form.studyYearId),
        semesterId: parseInt(form.semesterId),
      });
      toast.success('Registration submitted!');
      const myRes = await registrationService.getMyCourses().catch(() => []);
      setMyCourses(myRes?.data || myRes || []);
      setForm({ ...form, courseId: '' });
    } catch (err) {
      toast.error(
        err?.errorMessage || err?.errors?.join(', ') || 'Registration failed'
      );
    }
  };

  const handleCancel = async id => {
    if (!window.confirm('Cancel this registration?')) return;
    try {
      await registrationService.del(id);
      toast.success('Registration cancelled');
      const myRes = await registrationService.getMyCourses().catch(() => []);
      setMyCourses(myRes?.data || myRes || []);
    } catch (e) {
      toast.error('Failed');
    }
  };

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
          <FiClipboard style={{ marginRight: 8 }} />
          Course Registration
        </h1>
        <p>Register for courses in the current semester</p>
      </div>

      <div className="card" style={{ marginBottom: 20 }}>
        <h3 style={{ marginBottom: 16 }}>Register for a Course</h3>
        <form onSubmit={handleRegister}>
          <div className="form-row">
            <div className="form-group">
              <label>Study Year ID</label>
              <input
                type="number"
                className="form-control"
                value={form.studyYearId}
                onChange={e =>
                  setForm({ ...form, studyYearId: e.target.value })
                }
                required
              />
            </div>
            <div className="form-group">
              <label>Semester ID</label>
              <input
                type="number"
                className="form-control"
                value={form.semesterId}
                onChange={e => setForm({ ...form, semesterId: e.target.value })}
                required
              />
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Department ID (to load courses)</label>
              <div style={{ display: 'flex', gap: 8 }}>
                <input
                  type="number"
                  className="form-control"
                  id="deptIdInput"
                />
                <button
                  type="button"
                  className="btn btn-ghost"
                  onClick={() => {
                    const val = document.getElementById('deptIdInput').value;
                    if (val) loadDeptCourses(parseInt(val));
                  }}
                >
                  Load
                </button>
              </div>
            </div>
            <div className="form-group">
              <label>Course</label>
              <select
                className="form-control"
                value={form.courseId}
                onChange={e => setForm({ ...form, courseId: e.target.value })}
                required
              >
                <option value="">Select a course...</option>
                {deptCourses.map(c => (
                  <option key={c.id} value={c.id}>
                    {c.code} — {c.name} ({c.credits}cr)
                  </option>
                ))}
              </select>
            </div>
          </div>
          <button type="submit" className="btn btn-primary">
            <FiPlus /> Register
          </button>
        </form>
      </div>

      <div className="card">
        <h3 style={{ marginBottom: 16 }}>
          Current Registrations ({myCourses.length})
        </h3>
        {myCourses.length === 0 ? (
          <p style={{ color: 'var(--text-light)' }}>No registrations yet</p>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>Course</th>
                  <th>Status</th>
                  <th>Progress</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {myCourses.map(r => (
                  <tr key={r.id}>
                    <td>
                      <strong>{r.course?.code}</strong> — {r.course?.name}
                    </td>
                    <td>
                      <span
                        className={`badge ${r.status === 'Approved' ? 'badge-success' : r.status === 'Pending' ? 'badge-warning' : 'badge-danger'}`}
                      >
                        {r.status}
                      </span>
                    </td>
                    <td>
                      <span className="badge badge-info">{r.progress}</span>
                    </td>
                    <td>
                      {r.status === 'Pending' && (
                        <button
                          className="btn btn-danger btn-sm"
                          onClick={() => handleCancel(r.id)}
                        >
                          <FiTrash2 /> Cancel
                        </button>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
