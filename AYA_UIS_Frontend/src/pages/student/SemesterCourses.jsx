import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { FiBook, FiCheck, FiArrowLeft, FiChevronRight } from 'react-icons/fi';
import registrationService from '../../services/registrationService';
import { GRADE_LABELS } from '../../constants';
import { toast } from 'react-toastify';

export default function SemesterCourses() {
  const { studyYearId, semesterId } = useParams();
  const navigate = useNavigate();
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const res = await registrationService.getBySemester(studyYearId, semesterId);
        setCourses(res?.data || res || []);
      } catch (e) {
        toast.error('Failed to load courses');
      }
      setLoading(false);
    };
    load();
  }, [studyYearId, semesterId]);

  const statusBadge = s => {
    const map = { Approved: 'badge-success', Pending: 'badge-warning', Rejected: 'badge-danger', Suspended: 'badge-neutral' };
    return <span className={`badge ${map[s] || 'badge-neutral'}`}>{s}</span>;
  };

  const progressBadge = p => {
    const map = { Completed: 'badge-success', InProgress: 'badge-info', NotStarted: 'badge-neutral' };
    const labels = { Completed: 'Completed', InProgress: 'In Progress', NotStarted: 'Not Started' };
    return <span className={`badge ${map[p] || 'badge-neutral'}`}>{labels[p] || p}</span>;
  };

  if (loading) return <div className="page-container"><div className="spinner" /></div>;

  return (
    <div className="page-container">
      <div className="page-header">
        <button
          className="btn btn-ghost btn-sm"
          onClick={() => navigate(`/student/study-year/${studyYearId}/semesters`)}
          style={{ marginBottom: 12 }}
        >
          <FiArrowLeft /> Back to Semesters
        </button>
        <h1><FiBook style={{ marginRight: 8 }} />Semester Courses</h1>
        <p>Registered courses for this semester</p>
      </div>

      {courses.length === 0 ? (
        <div className="card empty-state">
          <h3>No registered courses</h3>
          <p>You don't have any registered courses in this semester.</p>
        </div>
      ) : (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(360px, 1fr))', gap: 20 }}>
          {courses.map(r => (
            <div
              className="card"
              key={r.id}
              style={{ cursor: 'pointer' }}
              onClick={() => navigate(`/student/course/${r.course?.id || r.courseId}/uploads`)}
            >
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'start', marginBottom: 12 }}>
                <div style={{ flex: 1 }}>
                  <h3 style={{ fontSize: '1rem', marginBottom: 2 }}>{r.course?.name || r.courseName}</h3>
                  <small style={{ color: 'var(--text-light)' }}>
                    {r.course?.code || r.courseCode} · {r.course?.credits || r.credits} Credits
                  </small>
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                  {r.isPassed && <FiCheck size={18} color="var(--success)" />}
                  <FiChevronRight size={18} color="var(--text-light)" />
                </div>
              </div>

              <div style={{ display: 'flex', gap: 8, flexWrap: 'wrap', marginBottom: 8 }}>
                {statusBadge(r.status)}
                {progressBadge(r.progress)}
                {r.grade && r.grade !== 'F' && <span className="badge badge-info">{GRADE_LABELS[r.grade] || r.grade}</span>}
                {r.grade === 'F' && <span className="badge badge-danger">F</span>}
              </div>

              {r.course?.description && (
                <p style={{ fontSize: '0.83rem', color: 'var(--text-light)', lineHeight: 1.5 }}>
                  {r.course.description.length > 120 ? r.course.description.slice(0, 120) + '...' : r.course.description}
                </p>
              )}

              {r.reason && (
                <p style={{ marginTop: 6, fontSize: '0.83rem', color: 'var(--text-light)' }}>
                  Note: {r.reason}
                </p>
              )}

              <div style={{ marginTop: 8, textAlign: 'right' }}>
                <span style={{ fontSize: '0.78rem', color: 'var(--info)' }}>View Uploads →</span>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
