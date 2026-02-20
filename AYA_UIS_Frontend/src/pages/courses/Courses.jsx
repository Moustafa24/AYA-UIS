import React, { useEffect, useState } from 'react';
import { FiPlus, FiBook, FiUpload, FiEye, FiList } from 'react-icons/fi';
import courseService from '../../services/courseService';
import departmentService from '../../services/departmentService';
import { toast } from 'react-toastify';

export default function Courses() {
  const [courses, setCourses] = useState([]);
  const [departments, setDepartments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal] = useState(null);
  const [form, setForm] = useState({
    code: '',
    name: '',
    credits: 1,
    departmentId: '',
  });
  const [detail, setDetail] = useState(null);

  const load = async () => {
    setLoading(true);
    try {
      const [cRes, dRes] = await Promise.all([
        courseService.getAll(),
        departmentService.getAll(),
      ]);
      setCourses(cRes?.data || cRes || []);
      setDepartments(dRes?.data || dRes || []);
    } catch (e) {
      toast.error('Failed to load');
    }
    setLoading(false);
  };

  useEffect(() => {
    load();
  }, []);

  const handleCreate = async e => {
    e.preventDefault();
    try {
      await courseService.create({
        ...form,
        credits: parseInt(form.credits),
        departmentId: parseInt(form.departmentId),
      });
      toast.success('Course created');
      setModal(null);
      load();
    } catch (err) {
      toast.error(err?.errorMessage || 'Failed');
    }
  };

  const viewUploads = async id => {
    try {
      const res = await courseService.getUploads(id);
      setDetail({ type: 'uploads', data: res?.data || res });
    } catch (e) {
      toast.error('Failed to load uploads');
    }
  };

  const viewPrereqs = async id => {
    try {
      const res = await courseService.getPrerequisites(id);
      setDetail({ type: 'prereqs', data: res?.data || res || [] });
    } catch (e) {
      toast.error('Failed to load prerequisites');
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
      <div
        className="page-header"
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
        }}
      >
        <div>
          <h1>
            <FiBook style={{ marginRight: 8 }} />
            Courses
          </h1>
          <p>Manage all courses</p>
        </div>
        <button
          className="btn btn-primary"
          onClick={() => {
            setForm({
              code: '',
              name: '',
              credits: 1,
              departmentId: departments[0]?.id || '',
            });
            setModal('create');
          }}
        >
          <FiPlus /> Add Course
        </button>
      </div>

      <div className="card">
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Code</th>
                <th>Name</th>
                <th>Credits</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {courses.length === 0 && (
                <tr>
                  <td colSpan={4} className="empty-state">
                    No courses found
                  </td>
                </tr>
              )}
              {courses.map(c => (
                <tr key={c.id}>
                  <td>
                    <strong>{c.code}</strong>
                  </td>
                  <td>{c.name}</td>
                  <td>{c.credits}</td>
                  <td style={{ display: 'flex', gap: 6 }}>
                    <button
                      className="btn btn-ghost btn-sm"
                      onClick={() => viewUploads(c.id)}
                      title="View Uploads"
                    >
                      <FiUpload />
                    </button>
                    <button
                      className="btn btn-ghost btn-sm"
                      onClick={() => viewPrereqs(c.id)}
                      title="Prerequisites"
                    >
                      <FiList />
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      {modal === 'create' && (
        <div className="modal-overlay" onClick={() => setModal(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Add Course</h2>
            <form onSubmit={handleCreate}>
              <div className="form-row">
                <div className="form-group">
                  <label>Code</label>
                  <input
                    className="form-control"
                    value={form.code}
                    onChange={e => setForm({ ...form, code: e.target.value })}
                    required
                  />
                </div>
                <div className="form-group">
                  <label>Name</label>
                  <input
                    className="form-control"
                    value={form.name}
                    onChange={e => setForm({ ...form, name: e.target.value })}
                    required
                  />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Credits</label>
                  <input
                    type="number"
                    className="form-control"
                    min={1}
                    value={form.credits}
                    onChange={e =>
                      setForm({ ...form, credits: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>Department</label>
                  <select
                    className="form-control"
                    value={form.departmentId}
                    onChange={e =>
                      setForm({ ...form, departmentId: e.target.value })
                    }
                    required
                  >
                    <option value="">Select...</option>
                    {departments.map(d => (
                      <option key={d.id} value={d.id}>
                        {d.name}
                      </option>
                    ))}
                  </select>
                </div>
              </div>
              <div className="form-actions">
                <button type="submit" className="btn btn-primary">
                  Create
                </button>
                <button
                  type="button"
                  className="btn btn-ghost"
                  onClick={() => setModal(null)}
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {detail && (
        <div className="modal-overlay" onClick={() => setDetail(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>
              {detail.type === 'uploads' ? 'Course Uploads' : 'Prerequisites'}
            </h2>
            {detail.type === 'uploads' &&
              (detail.data?.uploads?.length > 0 ? (
                <ul style={{ listStyle: 'none', padding: 0 }}>
                  {detail.data.uploads.map(u => (
                    <li
                      key={u.id}
                      style={{
                        padding: '10px 0',
                        borderBottom: '1px solid var(--border)',
                      }}
                    >
                      <strong>{u.title}</strong> —{' '}
                      <span style={{ color: 'var(--text-light)' }}>
                        {u.type}
                      </span>
                      <br />
                      <small>{u.description}</small>
                      {u.url && (
                        <a
                          href={u.url}
                          target="_blank"
                          rel="noreferrer"
                          className="btn btn-ghost btn-sm"
                          style={{ marginTop: 6 }}
                        >
                          View
                        </a>
                      )}
                    </li>
                  ))}
                </ul>
              ) : (
                <p className="empty-state">No uploads found</p>
              ))}
            {detail.type === 'prereqs' &&
              (detail.data?.length > 0 ? (
                <ul style={{ listStyle: 'none', padding: 0 }}>
                  {detail.data.map(c => (
                    <li
                      key={c.id}
                      style={{
                        padding: '8px 0',
                        borderBottom: '1px solid var(--border)',
                      }}
                    >
                      <strong>{c.code}</strong> — {c.name} ({c.credits} credits)
                    </li>
                  ))}
                </ul>
              ) : (
                <p className="empty-state">No prerequisites</p>
              ))}
            <div className="form-actions">
              <button className="btn btn-ghost" onClick={() => setDetail(null)}>
                Close
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
