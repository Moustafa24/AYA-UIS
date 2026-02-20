import React, { useEffect, useState } from 'react';
import { FiPlus, FiCalendar } from 'react-icons/fi';
import {
  studyYearService,
  semesterService,
} from '../../services/otherServices';
import { toast } from 'react-toastify';

export default function StudyYears() {
  const [studyYears, setStudyYears] = useState([]);
  const [semesters, setSemesters] = useState({});
  const [loading, setLoading] = useState(true);
  const [modal, setModal] = useState(null);
  const [yearForm, setYearForm] = useState({
    startYear: new Date().getFullYear(),
    endYear: new Date().getFullYear() + 1,
  });
  const [semForm, setSemForm] = useState({
    title: 'First_Semester',
    startDate: '',
    endDate: '',
    studyYearId: null,
  });

  const load = async () => {
    setLoading(true);
    // There's no getAll endpoint for study years, so we use semesters to discover years
    // For now we just show what we can create
    setLoading(false);
  };

  useEffect(() => {
    load();
  }, []);

  const loadSemesters = async yearId => {
    try {
      const res = await semesterService.getByYear(yearId);
      setSemesters(prev => ({ ...prev, [yearId]: res?.data || res || [] }));
    } catch (e) {
      toast.error('Failed to load semesters');
    }
  };

  const createYear = async e => {
    e.preventDefault();
    try {
      const res = await studyYearService.create({
        startYear: parseInt(yearForm.startYear),
        endYear: parseInt(yearForm.endYear),
      });
      toast.success(`Study year created (ID: ${res?.data || res})`);
      setModal(null);
    } catch (err) {
      toast.error(err?.errorMessage || 'Failed');
    }
  };

  const createSemester = async e => {
    e.preventDefault();
    try {
      await semesterService.create(semForm.studyYearId, {
        title: semForm.title,
        startDate: semForm.startDate,
        endDate: semForm.endDate,
      });
      toast.success('Semester created');
      setModal(null);
      loadSemesters(semForm.studyYearId);
    } catch (err) {
      toast.error(err?.errorMessage || 'Failed');
    }
  };

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
            <FiCalendar style={{ marginRight: 8 }} />
            Study Years & Semesters
          </h1>
          <p>Manage academic years</p>
        </div>
        <div style={{ display: 'flex', gap: 12 }}>
          <button className="btn btn-primary" onClick={() => setModal('year')}>
            <FiPlus /> New Study Year
          </button>
          <button
            className="btn btn-accent"
            onClick={() => setModal('semester')}
          >
            <FiPlus /> New Semester
          </button>
        </div>
      </div>

      <div className="card">
        <h3 style={{ marginBottom: 16 }}>Look up Semesters</h3>
        <div className="form-row" style={{ alignItems: 'end' }}>
          <div className="form-group">
            <label>Study Year ID</label>
            <input
              type="number"
              className="form-control"
              id="lookupYearId"
              min={1}
            />
          </div>
          <div className="form-group">
            <button
              className="btn btn-primary"
              onClick={() => {
                const id = document.getElementById('lookupYearId').value;
                if (id) loadSemesters(parseInt(id));
              }}
            >
              Load Semesters
            </button>
          </div>
        </div>
        {Object.entries(semesters).map(([yearId, sems]) => (
          <div key={yearId} style={{ marginTop: 20 }}>
            <h4>Study Year ID: {yearId}</h4>
            {sems.length === 0 ? (
              <p style={{ color: 'var(--text-light)' }}>No semesters found</p>
            ) : (
              <table style={{ marginTop: 8 }}>
                <thead>
                  <tr>
                    <th>Title</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                  </tr>
                </thead>
                <tbody>
                  {sems.map(s => (
                    <tr key={s.id}>
                      <td>{s.title}</td>
                      <td>{new Date(s.startDate).toLocaleDateString()}</td>
                      <td>{new Date(s.endDate).toLocaleDateString()}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        ))}
      </div>

      {modal === 'year' && (
        <div className="modal-overlay" onClick={() => setModal(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Create Study Year</h2>
            <form onSubmit={createYear}>
              <div className="form-row">
                <div className="form-group">
                  <label>Start Year</label>
                  <input
                    type="number"
                    className="form-control"
                    value={yearForm.startYear}
                    onChange={e =>
                      setYearForm({ ...yearForm, startYear: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>End Year</label>
                  <input
                    type="number"
                    className="form-control"
                    value={yearForm.endYear}
                    onChange={e =>
                      setYearForm({ ...yearForm, endYear: e.target.value })
                    }
                    required
                  />
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

      {modal === 'semester' && (
        <div className="modal-overlay" onClick={() => setModal(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Create Semester</h2>
            <form onSubmit={createSemester}>
              <div className="form-group">
                <label>Study Year ID</label>
                <input
                  type="number"
                  className="form-control"
                  value={semForm.studyYearId || ''}
                  onChange={e =>
                    setSemForm({
                      ...semForm,
                      studyYearId: parseInt(e.target.value),
                    })
                  }
                  required
                />
              </div>
              <div className="form-group">
                <label>Title</label>
                <select
                  className="form-control"
                  value={semForm.title}
                  onChange={e =>
                    setSemForm({ ...semForm, title: e.target.value })
                  }
                >
                  <option value="First_Semester">First Semester</option>
                  <option value="Second_Semester">Second Semester</option>
                  <option value="Summer">Summer</option>
                </select>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Start Date</label>
                  <input
                    type="date"
                    className="form-control"
                    value={semForm.startDate}
                    onChange={e =>
                      setSemForm({ ...semForm, startDate: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>End Date</label>
                  <input
                    type="date"
                    className="form-control"
                    value={semForm.endDate}
                    onChange={e =>
                      setSemForm({ ...semForm, endDate: e.target.value })
                    }
                    required
                  />
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
    </div>
  );
}
